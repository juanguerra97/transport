import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {
  MaterialClient, MaterialDto,
  TipoMaterialClient,
  TipoMaterialDto,
  UnidadMedidaClient,
  UnidadMedidaDto
} from "../../../web-api-client";
import {HotToastService} from "@ngneat/hot-toast";
import {ActivatedRoute} from "@angular/router";
import {getErrorMessage} from "../../../utils/errors";

@Component({
  selector: 'app-edit-material',
  templateUrl: './edit-material.component.html',
  styleUrls: ['./edit-material.component.scss']
})
export class EditMaterialComponent implements OnInit {

  form = new FormGroup({
    tipoMaterial: new FormControl(null, [Validators.required]),
    descripcion: new FormControl(null, [Validators.required, Validators.maxLength(256)]),
    detalle: new FormControl(null, [Validators.maxLength(4096)]),
    peso: new FormControl(null, [Validators.required, Validators.min(0)]),
    unidadMedida: new FormControl(null, [Validators.required]),
  });

  material?: MaterialDto;
  loadingMaterial = true;

  tiposMaterial: TipoMaterialDto[] = [];
  unidadesMedida: UnidadMedidaDto[] = [];

  guardando = false;

  constructor(
    private materialClient: MaterialClient,
    private tipoMaterialClient: TipoMaterialClient,
    private unidadMedidaClient: UnidadMedidaClient,
    private toastService: HotToastService,
    private ar: ActivatedRoute,
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

  async ngOnInit() {
    this.tiposMaterial = await this.cargarTiposMaterial();
    this.unidadesMedida = await this.cargarUnidadesMedida();
    this.ar.paramMap.subscribe({
      next: params => {
        const id = Number.parseInt(params.get('id') as any);
        this.cargarMaterial(id);
      }
    });
  }

  private cargarTiposMaterial() {
    return new Promise<TipoMaterialDto[]>(((resolve, reject) => {
      this.tipoMaterialClient.getTipoMateriales()
        .subscribe({
          next: res => {
            resolve(res);
          },
          error: error => {
            console.error(error);
            this.toastService.error(getErrorMessage(error), {
              dismissible: true,
              duration: 15000,
              position: 'bottom-center'
            });
            resolve([]);
          }
        });
    }));
  }

  private cargarUnidadesMedida() {
    return new Promise<UnidadMedidaDto[]>(((resolve, reject) => {
      this.unidadMedidaClient.getUnidadesMedida()
        .subscribe({
          next: res => {
            resolve(res);
          },
          error: error => {
            console.error(error);
            this.toastService.error(getErrorMessage(error), {
              dismissible: true,
              duration: 15000,
              position: 'bottom-center'
            });
            resolve([]);
          }
        });
    }));
  }

  private cargarMaterial(id: number) {
    this.loadingMaterial = true;
    this.materialClient.getMaterialById(id)
      .subscribe({
        next: async (res) => {
          this.material = res;
          this.form.patchValue({
            tipoMaterial: this.material.tipoMaterial,
            descripcion: this.material.descripcion,
            detalle: this.material.detalle,
            peso: this.material.peso,
            unidadMedida: this.material.unidadMedida
          });
          this.loadingMaterial = false;
        },
        error: error => {
          this.loadingMaterial = false;
          this.toastService.error(getErrorMessage(error), {
            position: 'bottom-center',
            dismissible: true,
            duration: 15000
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
    const datos = Object.assign({materialId: this.material?.id}, this.form.value);
    datos.tipoMaterialId = datos.tipoMaterial.id;
    datos.unidadMedidaId = datos.unidadMedida.id;
    delete datos.tipoMaterial;
    delete datos.unidadMedida;
    this.materialClient.update(this.material?.id as any, datos)
      .subscribe({
        next: res => {
          this.toastService.success('Se guardaron los datos.', {position: 'bottom-center'});
          this.form.enable();
          this.guardando = false;
        },
        error: error => {
          console.error(error);
          this.toastService.error(getErrorMessage(error), {
            autoClose: false,
            dismissible: true,
            position: "bottom-center"
          });
          this.form.enable();
          this.guardando = false;
        }
      });
  }

}
