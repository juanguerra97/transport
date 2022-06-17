import { Component, OnInit } from '@angular/core';
import {BodegaDto, MaterialClient, MaterialDto, PedidosClient} from "../../web-api-client";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {DynamicDialogConfig, DynamicDialogRef} from "primeng/dynamicdialog";
import {HotToastService} from "@ngneat/hot-toast";
import {getErrorMessage} from "../../utils/errors";

@Component({
  selector: 'app-new-pedido-material',
  templateUrl: './new-pedido-material.component.html',
  styleUrls: ['./new-pedido-material.component.scss']
})
export class NewPedidoMaterialComponent implements OnInit {

  bodega?: BodegaDto;

  form = new FormGroup({
    material: new FormControl(null, [Validators.required]),
    cantidad: new FormControl(1, [Validators.required]),
    detalle: new FormControl(null, [Validators.required, Validators.maxLength(1024)])
  });

  guardando = false;

  suggestionsMaterial: MaterialDto[] = [];

  constructor(
    private pedidosClient: PedidosClient,
    private materialClient: MaterialClient,
    private ref: DynamicDialogRef,
    private config: DynamicDialogConfig,
    private toastService: HotToastService,
  ) { }

  get material() {
    return this.form.get('material');
  }

  get cantidad() {
    return this.form.get('cantidad');
  }

  get detalle() {
    return this.form.get('detalle');
  }

  ngOnInit(): void {
    this.bodega = this.config.data;
  }

  guardar() {
    if (!this.form.valid) {
      this.form.markAllAsTouched();
      return;
    }
    this.guardando = true;
    this.form.disable();
    const datos = Object.assign({}, this.form.value);
    datos.bodegaSolicitaId = this.bodega?.id;
    datos.materialId = datos.material?.id;
    delete datos.material;
    this.pedidosClient.create(datos)
      .subscribe({
        next: res => {
          this.guardando = false;
          this.toastService.success('Se guardaron los datos.', {position: 'bottom-center'});
          this.ref.close(res);
        },
        error: error => {
          this.form.enable();
          this.guardando = false;
          console.error(error);
          this.toastService.error(getErrorMessage(error), {
            dismissible: true,
            duration: 15000,
            position: 'bottom-center'
          });
        }
      });
  }

  cancelar() {
    this.ref.close();
  }

  getMaterial(material: MaterialDto) {
    return `#${material.id} ${material.descripcion}`;
  }

  searchMaterial($event: any) {
    this.materialClient.searchMaterialesByDescripcion($event.query, 5)
      .subscribe({
        next: res => {
          this.suggestionsMaterial = res;
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

}
