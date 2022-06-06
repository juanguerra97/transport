import {Component, OnInit} from '@angular/core';
import {BodegaDto, BodegasClient} from "../../../web-api-client";
import {ConfirmationService} from "primeng/api";
import {HotToastService} from "@ngneat/hot-toast";
import {getErrorMessage} from "../../../utils/errors";

@Component({
  selector: 'app-home-bodegas',
  templateUrl: './home-bodegas.component.html',
  styleUrls: ['./home-bodegas.component.scss']
})
export class HomeBodegasComponent implements OnInit {

  bodegas: BodegaDto[] = [];
  loadingBodegas = true;

  constructor(
    private bodegasClient: BodegasClient,
    private confirmationService: ConfirmationService,
    private toastService: HotToastService,
  ) {
  }

  ngOnInit(): void {
    this.cargarBodegas();
  }

  private cargarBodegas() {
    this.loadingBodegas = true;
    this.bodegasClient.getBodegas()
      .subscribe({
        next: res => {
          this.bodegas = res;
          this.loadingBodegas = false;
        },
        error: error => {
          this.loadingBodegas = false;
          console.error(error);
          this.toastService.error(getErrorMessage(error), {
            dismissible: true,
            duration: 15000,
            position: 'bottom-center'
          });
        }
      })
  }

  confirmarEliminar(bodega: BodegaDto) {
    this.confirmationService.confirm({
      message: `¿Seguro que quieres eliminar la bodega No.${bodega.id}?`,
      acceptLabel: 'Sí, eliminar',
      rejectLabel: 'Cancelar',
      acceptButtonStyleClass: 'p-button-sm p-button-raised p-button-danger',
      rejectButtonStyleClass: 'p-button-sm p-button-raised',
      accept: () => {
        this.eliminar(bodega);
      }
    });
  }

  eliminar(bodega: BodegaDto) {
    this.bodegasClient.delete(bodega.id as any)
      .subscribe({
        next: res => {
          this.toastService.success(`Se eliminó la bodega No.${bodega.id}`, {position: 'bottom-center'});
          this.cargarBodegas();
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
