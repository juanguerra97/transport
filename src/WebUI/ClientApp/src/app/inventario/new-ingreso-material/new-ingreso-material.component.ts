import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {DynamicDialogConfig, DynamicDialogRef} from "primeng/dynamicdialog";
import {HotToastService} from "@ngneat/hot-toast";
import {
  BodegaDto,
  InventarioClient,
  MaterialClient,
  MaterialDto,
  ProveedorDto,
  ProveedoresClient
} from "../../web-api-client";
import {getErrorMessage} from "../../utils/errors";

@Component({
  selector: 'app-new-ingreso-material',
  templateUrl: './new-ingreso-material.component.html',
  styleUrls: ['./new-ingreso-material.component.scss']
})
export class NewIngresoMaterialComponent implements OnInit {

  bodega?: BodegaDto;

  form = new FormGroup({
    proveedor: new FormControl(null, [Validators.required]),
    material: new FormControl(null, [Validators.required]),
    cantidad: new FormControl(1, [Validators.required]),
  });

  guardando = false;

  suggestionsProveedor: ProveedorDto[] = [];
  suggestionsMaterial: MaterialDto[] = [];

  constructor(
    private inventarioClient: InventarioClient,
    private proveedoresClient: ProveedoresClient,
    private materialClient: MaterialClient,
    private ref: DynamicDialogRef,
    private config: DynamicDialogConfig,
    private toastService: HotToastService,
  ) { }

  get proveedor() {
    return this.form.get('proveedor');
  }

  get material() {
    return this.form.get('material');
  }

  get cantidad() {
    return this.form.get('cantidad');
  }

  ngOnInit(): void {
    this.bodega = this.config.data
  }

  guardar() {
    if (!this.form.valid) {
      this.form.markAllAsTouched();
      return;
    }
    this.guardando = true;
    this.form.disable();
    const datos = Object.assign({}, this.form.value);
    datos.bodegaId = this.bodega?.id;
    datos.proveedorMaterialId = datos.proveedor?.id;
    datos.materialId = datos.material?.id;
    delete datos.proveedor;
    delete datos.material;
    this.inventarioClient.create(datos)
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

  getProveedor(proveedor: ProveedorDto) {
    return `${proveedor.nombre} (${proveedor.nit})`;
  }

  searchProveedor($event: any) {
    this.proveedoresClient.searchProveedoresByName($event.query, 5)
      .subscribe({
        next: res => {
          this.suggestionsProveedor = res;
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
