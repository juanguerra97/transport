import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {
  BodegaDto,
  BodegasClient,
  DepartamentoDto, DepartamentosClient,
  MunicipioDto, MunicipiosClient, UsuarioClient, UsuarioDto,
} from "../../../web-api-client";
import {HotToastService} from "@ngneat/hot-toast";
import {ActivatedRoute} from "@angular/router";
import {getErrorMessage} from "../../../utils/errors";
import {Paises} from "../../../utils/constants";

@Component({
  selector: 'app-edit-bodega',
  templateUrl: './edit-bodega.component.html',
  styleUrls: ['./edit-bodega.component.scss']
})
export class EditBodegaComponent implements OnInit {

  form = new FormGroup({
    descripcion: new FormControl(null, [Validators.required, Validators.maxLength(256)]),
    detalle: new FormControl(null, [Validators.required, Validators.maxLength(1024)]),
    direccion: new FormControl(null, [Validators.required, Validators.maxLength(256)]),
    departamento: new FormControl(null, [Validators.required]),
    municipio: new FormControl(null, [Validators.required]),
    encargado: new FormControl(null, [Validators.required]),
  });

  bodega?: BodegaDto;
  loadingBodega = true;

  departamentos: DepartamentoDto[] = [];
  municipios: MunicipioDto[] = [];

  suggestionsEncargado: UsuarioDto[] = [];

  guardando = false;

  constructor(
    private bodegasClient: BodegasClient,
    private departamentosClient: DepartamentosClient,
    private municipiosClient: MunicipiosClient,
    private usuarioClient: UsuarioClient,
    private toastService: HotToastService,
    private ar: ActivatedRoute,
  ) {
  }

  get descripcion() {
    return this.form.get('descripcion');
  }

  get detalle() {
    return this.form.get('detalle');
  }

  get direccion() {
    return this.form.get('direccion');
  }

  get departamento() {
    return this.form.get('departamento');
  }

  get municipio() {
    return this.form.get('municipio');
  }

  get encargado() {
    return this.form.get('encargado');
  }

  async ngOnInit() {
    this.ar.paramMap.subscribe({
      next: params => {
        const id = Number.parseInt(params.get('id') as any);
        this.cargarBodega(id);
      }
    });
  }

  private cargarBodega(id: number) {
    this.loadingBodega = true;
    this.bodegasClient.getBodegaById(id)
      .subscribe({
        next: async (res) => {
          this.bodega = res;

          this.departamentos = await this.cargarDepartamentos(Paises.GUATEMALA_ID);
          this.municipios = await this.cargarMunicipios(this.bodega.ubicacion?.municipio?.departamento?.id);

          this.form.patchValue({
            descripcion: this.bodega.descripcion,
            detalle: this.bodega.detalle,
            direccion: this.bodega.ubicacion?.direccion,
            departamento: this.bodega.ubicacion?.municipio?.departamento,
            municipio: this.bodega.ubicacion?.municipio,
            encargado: this.bodega.encargado,
          });

          this.loadingBodega = false;
        },
        error: error => {
          this.loadingBodega = false;
          this.toastService.error(getErrorMessage(error), {position: 'bottom-center', dismissible: true, duration: 15000});
        }
      });
  }

  private cargarDepartamentos(paisId?: number) {
    return new Promise<DepartamentoDto[]>(((resolve, reject) => {
      this.departamentosClient.getDepartamentos(paisId)
        .subscribe({
          next: res => {
            resolve(res);
          },
          error: error => {
            console.error(error);
            this.toastService.error(getErrorMessage(error), {position: 'bottom-center', dismissible: true, duration: 15000});
            resolve([]);
          }
        });
    }));
  }

  async onChangeDepartamento($event: any) {
    const value = $event.value as DepartamentoDto | undefined;
    this.municipios = [];
    this.municipio?.reset();
    if (value) {
      this.municipios = await this.cargarMunicipios(value.id);
    }
  }

  private cargarMunicipios(departamentoId?: number) {
    return new Promise<MunicipioDto[]>(((resolve, reject) => {
      this.municipiosClient.getMunicipios(undefined, departamentoId)
        .subscribe({
          next: res => {
            resolve(res);
          },
          error: error => {
            console.error(error);
            this.toastService.error(getErrorMessage(error), {position: 'bottom-center', dismissible: true, duration: 15000});
            resolve([]);
          }
        });
    }));
  }

  guardar() {
    if (!this.form.valid) {
      this.form.markAllAsTouched();
      return;
    }
    this.guardando = true;
    this.form.disable();
    const datos = Object.assign({plantaId: this.bodega?.id}, this.form.value);
    datos.municipioId = datos.municipio.id;
    datos.encargadoId = datos.encargado.id;
    delete datos.departamento;
    delete datos.municipio;
    delete datos.encargado;
    this.bodegasClient.update(this.bodega?.id as any, datos)
      .subscribe({
        next: res => {
          this.toastService.success('Se guardaron los datos.', { position: 'bottom-center'});
          this.form.enable();
          this.guardando = false;
        },
        error: error => {
          console.error(error);
          this.toastService.error(getErrorMessage(error), { autoClose: false, dismissible: true, position: "bottom-center"});
          this.form.enable();
          this.guardando = false;
        }
      });
  }

  searchEncargado($event: any) {
    this.usuarioClient.searchUsuariosByName($event.query, 5)
      .subscribe({
        next: res => {
          this.suggestionsEncargado = res;
        },
        error: error => {
          console.error(error);
          this.toastService.error(getErrorMessage(error), { autoClose: false, dismissible: true, position: "bottom-center"});
        }
      });
  }

  getEncargado(usuario: UsuarioDto) {
    return `${usuario.firstName} ${usuario.lastName}(${usuario.userName})`;
  }
}
