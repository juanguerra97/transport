import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {
  DepartamentoDto,
  DepartamentosClient,
  MunicipioDto, MunicipiosClient,
  PlantasClient, TipoPlantaClient,
  TipoPlantaDto, UsuarioClient, UsuarioDto
} from "../../../web-api-client";
import {HotToastService} from "@ngneat/hot-toast";
import {getErrorMessage} from "../../../utils/errors";
import {Router} from "@angular/router";
import {Paises} from "../../../utils/constants";

@Component({
  selector: 'app-new-planta',
  templateUrl: './new-planta.component.html',
  styleUrls: ['./new-planta.component.scss']
})
export class NewPlantaComponent implements OnInit {

  form = new FormGroup({
    tipoPlanta: new FormControl(null, [Validators.required]),
    descripcion: new FormControl(null, [Validators.required, Validators.maxLength(256)]),
    detalle: new FormControl(null, [Validators.required, Validators.maxLength(1024)]),
    direccion: new FormControl(null, [Validators.required, Validators.maxLength(256)]),
    departamento: new FormControl(null, [Validators.required]),
    municipio: new FormControl(null, [Validators.required]),
    encargado: new FormControl(null, [Validators.required])
  });

  tiposPlanta: TipoPlantaDto[] = [];
  departamentos: DepartamentoDto[] = [];
  municipios: MunicipioDto[] = [];

  suggestionsEncargado: UsuarioDto[] = [];

  guardando = false;

  constructor(
    private plantasClient: PlantasClient,
    private tipoPlantaClient: TipoPlantaClient,
    private departamentosClient: DepartamentosClient,
    private municipiosClient: MunicipiosClient,
    private usuarioClient: UsuarioClient,
    private toastService: HotToastService,
    private router: Router,
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

  get encargado() {
    return this.form.get('encargado');
  }

  ngOnInit(): void {
    this.cargarTiposPlanta();
    this.cargarDepartamentos(Paises.GUATEMALA_ID);
  }

  private cargarTiposPlanta() {
    this.tipoPlantaClient.getTipoPlantas()
      .subscribe({
        next: res => {
          this.tiposPlanta = res;
        },
        error: error => {
          console.error(error);
        }
      });
  }

  private cargarDepartamentos(paisId?: number) {
    this.municipios = [];
    this.departamentosClient.getDepartamentos(paisId)
      .subscribe({
        next: res => {
          this.departamentos = res;
        },
        error: error => {
          console.error(error);
        }
      });
  }

  onChangeDepartamento($event: any) {
    const value = $event.value as DepartamentoDto | undefined;
    if (!value) {
      this.municipios = [];
    } else {
      this.cargarMunicipios(value.id);
    }
    this.municipio?.reset();
  }

  private cargarMunicipios(departamentoId?: number) {
    this.municipiosClient.getMunicipios(undefined, departamentoId)
      .subscribe({
        next: res => {
          this.municipios = res;
        },
        error: error => {
          console.error(error);
        }
      });
  }

  guardar() {
    if (!this.form.valid) {
      this.form.markAllAsTouched();
      return;
    }
    this.guardando = true;
    this.form.disable();
    const datos = Object.assign({}, this.form.value);
    datos.municipioId = datos.municipio.id;
    datos.tipoPlantaId = datos.tipoPlanta.id;
    datos.encargadoId = datos.encargado?.id;
    delete datos.departamento;
    delete datos.municipio;
    delete datos.tipoPlanta;
    delete datos.encargado;
    this.plantasClient.create(datos)
      .subscribe({
        next: res => {
          this.toastService.success('Se guardaron los datos.', { position: 'bottom-center'});
          this.guardando = false;
          this.router.navigateByUrl('/admin/catalogo/plantas');
        },
        error: error => {
          console.error(error);
          this.form.enable();
          this.toastService.error(getErrorMessage(error), { autoClose: false, dismissible: true, position: "bottom-center"});
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
