import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {
  MaterialClient, TipoMaterialClient, TipoMaterialDto, UnidadMedidaClient, UnidadMedidaDto
} from "../../../web-api-client";
import {HotToastService} from "@ngneat/hot-toast";
import {Router} from "@angular/router";
import {getErrorMessage} from "../../../utils/errors";

@Component({
  selector: 'app-new-material',
  templateUrl: './new-material.component.html',
  styleUrls: ['./new-material.component.scss']
})
export class NewMaterialComponent implements OnInit {

  form = new FormGroup({
    tipoMaterial: new FormControl(null, [Validators.required]),
    descripcion: new FormControl(null, [Validators.required, Validators.maxLength(256)]),
    detalle: new FormControl(null, [Validators.maxLength(4096)]),
    peso: new FormControl(null, [Validators.required, Validators.min(0)]),
    unidadMedida: new FormControl(null, [Validators.required]),
  });

  tiposMaterial: TipoMaterialDto[] = [];
  unidadesMedida: UnidadMedidaDto[] = [];

  guardando = false;

  constructor(
    private materialClient: MaterialClient,
    private tipoMaterialClient: TipoMaterialClient,
    private unidadMedidaClient: UnidadMedidaClient,
    private toastService: HotToastService,
    private router: Router,
  ) {
  }

  get tipoMaterial() {
    return this.form.get('tipoMaterial');
  }

  get descripcion() {
    return this.form.get('descripcion');
  }

  get detalle() {
    return this.form.get('detalle');
  }

  get peso() {
    return this.form.get('peso');
  }

  get unidadMedida() {
    return this.form.get('unidadMedida');
  }

  ngOnInit(): void {
    this.cargarTiposMaterial();
    this.cargarUnidadesMedida();
  }

  private cargarTiposMaterial() {
    this.tipoMaterialClient.getTipoMateriales()
      .subscribe({
        next: res => {
          this.tiposMaterial = res;
        },
        error: error => {
          console.error(error);
          this.toastService.error(getErrorMessage(error), {
            dismissible: true,
            duration: 15000,
            position: 'bottom-center'
          });
        }
      });
  }

  private cargarUnidadesMedida() {
    this.unidadMedidaClient.getUnidadesMedida()
      .subscribe({
        next: res => {
          this.unidadesMedida = res;
        },
        error: error => {
          console.error(error);
          this.toastService.error(getErrorMessage(error), {
            dismissible: true,
            duration: 15000,
            position: 'bottom-center'
          });
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
    datos.tipoMaterialId = datos.tipoMaterial.id;
    datos.unidadMedidaId = datos.unidadMedida.id;
    delete datos.tipoMaterial;
    delete datos.unidadMedida;
    this.materialClient.create(datos)
      .subscribe({
        next: res => {
          this.toastService.success('Se guardaron los datos.', {position: 'bottom-center'});
          this.guardando = false;
          this.router.navigateByUrl('/admin/catalogo/materiales');
        },
        error: error => {
          console.error(error);
          this.toastService.error(getErrorMessage(error), {
            autoClose: false,
            dismissible: true,
            position: "bottom-center"
          });
          this.guardando = false;
        }
      });
  }

}
