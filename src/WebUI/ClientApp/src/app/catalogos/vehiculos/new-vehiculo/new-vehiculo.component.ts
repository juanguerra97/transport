import {Component, OnInit} from '@angular/core';
import {DynamicDialogConfig, DynamicDialogRef} from "primeng/dynamicdialog";
import {HotToastService} from "@ngneat/hot-toast";
import {VehiculoDto, VehiculosClient} from "../../../web-api-client";
import {getErrorMessage} from "../../../utils/errors";

@Component({
  selector: 'app-new-vehiculo',
  templateUrl: './new-vehiculo.component.html',
  styleUrls: ['./new-vehiculo.component.scss']
})
export class NewVehiculoComponent implements OnInit {

  guardando = false;

  constructor(
    private vehiculosClient: VehiculosClient,
    private ref: DynamicDialogRef,
    private config: DynamicDialogConfig,
    private toastService: HotToastService,
  ) {
  }

  ngOnInit(): void {
  }

  guardar(vehiculo: VehiculoDto) {
    this.guardando = true;
    vehiculo.esUsoInterno = true;
    this.vehiculosClient.create(vehiculo)
      .subscribe({
        next: res => {
          this.guardando = false;
          this.toastService.success('Se guardaron los datos.', {position: 'bottom-center'});
          this.ref.close(res);
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
