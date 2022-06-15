import {ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {BodegasClient, IPaginatedListOfBodegaDto} from "../../web-api-client";
import {getErrorMessage} from "../../utils/errors";
import {HotToastService} from "@ngneat/hot-toast";

@Component({
  selector: 'app-home-inventario',
  templateUrl: './home-inventario.component.html',
  styleUrls: ['./home-inventario.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HomeInventarioComponent implements OnInit {

  TIPOS_BODEGA_LABEL: any = {
    0: 'BODEGA',
    1: 'PLANTA'
  };

  bodegas: IPaginatedListOfBodegaDto = {
    totalCount: 0,
    pageSize: 10,
    pageNumber: 1,
    totalPages: 0,
    items: [],
    hasNextPage: false,
    hasPreviousPage: false,
  };
  loading = true;

  constructor(
    private bodegasClient: BodegasClient,
    private toastService: HotToastService,
    private cdr: ChangeDetectorRef,
  ) {
  }

  ngOnInit(): void {
    this.cargarBodegas();
  }

  private cargarBodegas(pageSize: number = 10, pageNumber: number = 1) {
    this.loading = true;
    this.cdr.markForCheck();
    this.bodegasClient.getBodegasByEncargado(pageSize, pageNumber)
      .subscribe({
        next: res => {
          this.bodegas = res;
          this.loading = false;
          this.cdr.markForCheck();
        },
        error: error => {
          console.error(error);
          this.loading = false;
          this.cdr.markForCheck();
          this.toastService.error(getErrorMessage(error), {
            dismissible: true,
            duration: 15000,
            position: 'bottom-center'
          });
        }
      });
  }

  paginar($event: any) {
    const pageNumber = ($event.page || 0) + 1;
    const pageSize = $event.rows;
    this.cargarBodegas(pageSize, pageNumber);
  }

}
