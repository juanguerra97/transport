import { Component, OnInit } from '@angular/core';
import {UnidadMedidaClient, UnidadMedidaDto} from "../../../web-api-client";
import {DynamicDialogConfig, DynamicDialogRef} from "primeng/dynamicdialog";
import {HotToastService} from "@ngneat/hot-toast";
import {getErrorMessage} from "../../../utils/errors";

@Component({
  selector: 'app-edit-unidad-medida',
  templateUrl: './edit-unidad-medida.component.html',
  styleUrls: ['./edit-unidad-medida.component.scss']
})
export class EditUnidadMedidaComponent implements OnInit {

  medida?: UnidadMedidaDto;
  guardando = false;

  constructor(
    private unidadMedidaClient: UnidadMedidaClient,
    private ref: DynamicDialogRef,
    private config: DynamicDialogConfig,
    private toastService: HotToastService,
  ) { }

  ngOnInit(): void {
    this.medida = this.config.data;
  }

  guardar(medida: UnidadMedidaDto) {
    this.guardando = true;
    medida.id = this.medida?.id;
    this.unidadMedidaClient.update(medida.id as any, medida)
      .subscribe({
        next: res => {
          this.guardando = false;
          this.toastService.success('Se guardaron los datos.', {position: 'bottom-center'});
          // this.ref.close(res);
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
