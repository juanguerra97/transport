import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {
  DepartamentoDto, DepartamentosClient,
  MunicipioDto, MunicipiosClient,
  PlantaDto,
  PlantasClient,
  TipoPlantaClient,
  TipoPlantaDto
} from "../../../web-api-client";
import {HotToastService} from "@ngneat/hot-toast";
import {getErrorMessage} from "../../../utils/errors";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Paises} from "../../../utils/constants";

@Component({
  selector: 'app-edit-planta',
  templateUrl: './edit-planta.component.html',
  styleUrls: ['./edit-planta.component.scss']
})
export class EditPlantaComponent implements OnInit {

  form = new FormGroup({
    tipoPlanta: new FormControl(null, [Validators.required]),
    descripcion: new FormControl(null, [Validators.required, Validators.maxLength(256)]),
    detalle: new FormControl(null, [Validators.required, Validators.maxLength(1024)]),
    direccion: new FormControl(null, [Validators.required, Validators.maxLength(256)]),
    departamento: new FormControl(null, [Validators.required]),
    municipio: new FormControl(null, [Validators.required]),
  });

  planta?: PlantaDto;
  loadingPlanta = true;

  tiposPlanta: TipoPlantaDto[] = [];
  departamentos: DepartamentoDto[] = [];
  municipios: MunicipioDto[] = [];

  guardando = false;

  constructor(
    private plantasClient: PlantasClient,
    private tipoPlantaClient: TipoPlantaClient,
    private departamentosClient: DepartamentosClient,
    private municipiosClient: MunicipiosClient,
    private toastService: HotToastService,
    private ar: ActivatedRoute,
  ) {
  }

  get tipoPlanta() {
    return this.form.get('tipoPlanta');
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

  async ngOnInit() {
    this.tiposPlanta = await this.cargarTiposPlanta();
    this.ar.paramMap.subscribe({
      next: params => {
        const id = Number.parseInt(params.get('id') as any);
        this.cargarPlanta(id);
      }
    });
  }

  private cargarTiposPlanta() {
    return new Promise<TipoPlantaDto[]>(((resolve, reject) => {
      this.tipoPlantaClient.getTipoPlantas()
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

  private cargarPlanta(id: number) {
    this.loadingPlanta = true;
    this.plantasClient.getPlantaById(id)
      .subscribe({
        next: async (res) => {
          this.planta = res;

          this.departamentos = await this.cargarDepartamentos(Paises.GUATEMALA_ID);
          this.municipios = await this.cargarMunicipios(this.planta.bodega?.ubicacion?.municipio?.departamento?.id);

          this.form.patchValue({
            tipoPlanta: this.planta.tipoPlanta,
            descripcion: this.planta.descripcion,
            detalle: this.planta.detalle,
            direccion: this.planta.bodega?.ubicacion?.direccion,
            departamento: this.planta.bodega?.ubicacion?.municipio?.departamento,
            municipio: this.planta.bodega?.ubicacion?.municipio,
          });

          this.loadingPlanta = false;
        },
        error: error => {
          this.loadingPlanta = false;
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
    const datos = Object.assign({plantaId: this.planta?.id}, this.form.value);
    datos.municipioId = datos.municipio.id;
    datos.tipoPlantaId = datos.tipoPlanta.id;
    delete datos.departamento;
    delete datos.municipio;
    delete datos.tipoPlanta;
    this.plantasClient.update(this.planta?.id as any, datos)
      .subscribe({
        next: res => {
          this.toastService.success('Se guardaron los datos.', { position: 'bottom-center'});
          this.guardando = false;
        },
        error: error => {
          console.error(error);
          this.toastService.error(getErrorMessage(error), { autoClose: false, dismissible: true, position: "bottom-center"});
          this.guardando = false;
        }
      });
  }

}
