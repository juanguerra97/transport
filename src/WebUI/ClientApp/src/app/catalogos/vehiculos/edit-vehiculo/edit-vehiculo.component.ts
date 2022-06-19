import {Component, OnInit} from '@angular/core';
import {VehiculoDto, VehiculosClient} from "../../../web-api-client";
import {DynamicDialogConfig, DynamicDialogRef} from "primeng/dynamicdialog";
import {HotToastService} from "@ngneat/hot-toast";
import {getErrorMessage} from "../../../utils/errors";

@Component({
  selector: 'app-edit-vehiculo',
  templateUrl: './edit-vehiculo.component.html',
  styleUrls: ['./edit-vehiculo.component.scss']
})
export class EditVehiculoComponent implements OnInit {

  vehiculo?: VehiculoDto;
  guardando = false;

  constructor(
    private vehiculosClient: VehiculosClient,
    private ref: DynamicDialogRef,
    private config: DynamicDialogConfig,
    private toastService: HotToastService,
  ) {
  }

  ngOnInit(): void {
    this.vehiculo = this.config.data;
  }

  guardar(vehiculo: VehiculoDto) {
    this.guardando = true;
    vehiculo.id = this.vehiculo?.id;
    vehiculo.esUsoInterno = this.vehiculo?.esUsoInterno;
    this.vehiculosClient.update(vehiculo.id as any, vehiculo)
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
