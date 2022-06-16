import { Component, OnInit } from '@angular/core';
import {ProveedorDto, ProveedoresClient} from "../../../web-api-client";
import {DynamicDialogConfig, DynamicDialogRef} from "primeng/dynamicdialog";
import {HotToastService} from "@ngneat/hot-toast";
import {getErrorMessage} from "../../../utils/errors";

@Component({
  selector: 'app-edit-proveedor',
  templateUrl: './edit-proveedor.component.html',
  styleUrls: ['./edit-proveedor.component.scss']
})
export class EditProveedorComponent implements OnInit {

  proveedor?: ProveedorDto;
  guardando = false;

  constructor(
    private proveedoresClient: ProveedoresClient,
    private ref: DynamicDialogRef,
    private config: DynamicDialogConfig,
    private toastService: HotToastService,
  ) { }

  ngOnInit(): void {
    this.proveedor = this.config.data;
  }

  guardar(proveedor: ProveedorDto) {
    this.guardando = true;
    proveedor.id = this.proveedor?.id;
    this.proveedoresClient.update(proveedor.id as any, proveedor)
      .subscribe({
        next: res => {
          this.guardando = false;
          this.toastService.success('Se guardaron los datos.', {position: 'bottom-center'});
        },
        error: error => {
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

  cancelar($event: any) {
    this.ref.close($event);
  }

}
