import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {MaterialDto, PedidoMaterialDto, PedidosClient} from "../../web-api-client";
import {DynamicDialogConfig, DynamicDialogRef} from "primeng/dynamicdialog";
import {HotToastService} from "@ngneat/hot-toast";
import {getErrorMessage} from "../../utils/errors";

@Component({
  selector: 'app-edit-pedido-material',
  templateUrl: './edit-pedido-material.component.html',
  styleUrls: ['./edit-pedido-material.component.scss']
})
export class EditPedidoMaterialComponent implements OnInit {

  pedido?: PedidoMaterialDto;

  form = new FormGroup({
    material: new FormControl(null, [Validators.required]),
    cantidad: new FormControl(1, [Validators.required]),
    detalle: new FormControl(null, [Validators.required, Validators.maxLength(1024)])
  });

  guardando = false;

  constructor(
    private pedidosClient: PedidosClient,
    private ref: DynamicDialogRef,
    private config: DynamicDialogConfig,
    private toastService: HotToastService,
  ) {
  }

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
    this.pedido = this.config.data;
    if (this.pedido) {
      this.form.patchValue(this.pedido);
      this.material?.disable();
    }
  }

  guardar() {
    if (!this.form.valid) {
      this.form.markAllAsTouched();
      return;
    }
    this.guardando = true;
    this.form.disable();
    const datos = Object.assign({}, this.form.value);
    datos.pedidoMaterialId = this.pedido?.id;
    delete datos.material;
    this.pedidosClient.update(this.pedido?.id as any, datos)
      .subscribe({
        next: res => {
          this.guardando = false;
          this.toastService.success('Se guardaron los datos.', {position: 'bottom-center'});
          this.ref.close(res);
        },
        error: error => {
          this.form.enable();
          this.material?.disable();
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

}
