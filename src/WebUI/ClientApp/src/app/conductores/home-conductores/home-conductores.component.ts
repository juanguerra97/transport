import {ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {
  IPaginatedListOfMovimientoBodegaDto,
  MovimientoBodegaClient,
  MovimientoBodegaDto,
  VehiculoDto
} from "../../web-api-client";
import {HotToastService} from "@ngneat/hot-toast";
import {getErrorMessage} from "../../utils/errors";
import {ConfirmationService} from "primeng/api";
import {EstadosMovimiento} from "../../utils/constants";

@Component({
  selector: 'app-home-conductores',
  templateUrl: './home-conductores.component.html',
  styleUrls: ['./home-conductores.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HomeConductoresComponent implements OnInit {

  movimientos: IPaginatedListOfMovimientoBodegaDto = {
    totalCount: 0,
    pageSize: 10,
    pageNumber: 1,
    totalPages: 0,
    items: [],
    hasNextPage: false,
    hasPreviousPage: false,
  };
  loadingMovimientos = true;

  constructor(
    private movimientoBodegaClient: MovimientoBodegaClient,
    private confirmationService: ConfirmationService,
    private toastService: HotToastService,
    private cdr: ChangeDetectorRef,
  ) {
  }

  ngOnInit(): void {
    this.cargarMovimientos();
  }

  private cargarMovimientos(pageSize: number = 10, pageNumber = 1, descripcionMaterial: string | undefined = undefined, bodegaOrigenId: number | undefined = undefined, bodegaDestinoId: number | undefined = undefined) {
    this.loadingMovimientos = true;
    this.cdr.markForCheck();
    this.movimientoBodegaClient.getMovimientosBodegaByConductor(pageSize, pageNumber, descripcionMaterial, bodegaOrigenId, bodegaDestinoId)
      .subscribe({
        next: res => {
          this.movimientos = res;
          this.loadingMovimientos = false;
          this.cdr.markForCheck();
        },
        error: error => {
          console.error(error);
          this.loadingMovimientos = false;
          this.cdr.markForCheck();
          this.toastService.error(getErrorMessage(error), {
            dismissible: true,
            duration: 10000,
            position: 'bottom-center'
          });
        }
      });
  }

  paginar($event: any) {
    const pageNumber = ($event.page || 0) + 1;
    const pageSize = $event.rows;
    this.cargarMovimientos(pageSize, pageNumber);
  }

  confirmarCargar(movimiento: MovimientoBodegaDto) {
    this.confirmationService.confirm({
      message: `¿Seguro que quieres marcar la entrega como CARGADA?`,
      acceptLabel: 'Sí, cargar',
      rejectLabel: 'Cancelar',
      acceptButtonStyleClass: 'p-button-sm p-button-raised p-button-danger',
      rejectButtonStyleClass: 'p-button-sm p-button-raised',
      accept: () => {
        this.cargar(movimiento);
      }
    });
  }

  cargar(movimiento: MovimientoBodegaDto) {
    this.movimientoBodegaClient.cargarMovimiento(movimiento.id as any)
      .subscribe({
        next: res => {
          movimiento.estadoMovimientoBodega = res.estadoMovimientoBodega;
          this.cdr.markForCheck();
          this.toastService.success(`La entrega se marco como cargada..`, {position: 'bottom-center'});
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

  isProgramado(movimiento: MovimientoBodegaDto) {
    return movimiento.estadoMovimientoBodega?.id === EstadosMovimiento.PROGRAMADO_ID;
  }

}
