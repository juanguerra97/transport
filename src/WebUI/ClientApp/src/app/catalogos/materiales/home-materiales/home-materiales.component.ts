import {ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {
  IPaginatedListOfMaterialDto,
  MaterialClient, MaterialDto,
  TipoMaterialClient,
  TipoMaterialDto
} from "../../../web-api-client";
import {getErrorMessage} from "../../../utils/errors";
import {HotToastService} from "@ngneat/hot-toast";
import {ConfirmationService} from "primeng/api";

@Component({
  selector: 'app-home-materiales',
  templateUrl: './home-materiales.component.html',
  styleUrls: ['./home-materiales.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HomeMaterialesComponent implements OnInit {

  materiales: IPaginatedListOfMaterialDto = {
    totalCount: 0,
    pageSize: 10,
    pageNumber: 1,
    totalPages: 0,
    items: [],
    hasNextPage: false,
    hasPreviousPage: false,
  };
  loadingMateriales = true;

  tiposMaterial: TipoMaterialDto[] = [];
  selectedTipoMaterial?: TipoMaterialDto;

  constructor(
    private materialClient: MaterialClient,
    private tipoMaterialClient: TipoMaterialClient,
    private confirmationClient: ConfirmationService,
    private toastService: HotToastService,
    private cdr: ChangeDetectorRef,
  ) {
  }

  ngOnInit(): void {
    this.cargarTiposMaterial();
    this.cargarMateriales();
  }

  private cargarTiposMaterial() {
    this.tipoMaterialClient.getTipoMateriales()
      .subscribe({
        next: res => {
          this.tiposMaterial = res;
          this.cdr.markForCheck();
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

  private cargarMateriales(pageSize: number = 10, pageNumber: number = 1, tipoMaterialId?: number) {
    this.loadingMateriales = true;
    this.cdr.markForCheck();
    this.materialClient.getMateriales(pageSize, pageNumber, tipoMaterialId)
      .subscribe({
        next: res => {
          this.materiales = res;
          this.loadingMateriales = false;
          this.cdr.markForCheck();
        },
        error: error => {
          console.error(error);
          this.loadingMateriales = false;
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
    this.cargarMateriales(pageSize, pageNumber, this.selectedTipoMaterial?.id);
  }

  confirmarEliminar(material: MaterialDto) {
    this.confirmationClient.confirm({
      message: `¿Seguro que quieres eliminar el material No.${material.id}?`,
      acceptLabel: 'Sí, eliminar',
      rejectLabel: 'Cancelar',
      acceptButtonStyleClass: 'p-button-sm p-button-raised p-button-danger',
      rejectButtonStyleClass: 'p-button-sm p-button-raised',
      accept: () => {
        this.eliminar(material);
      }
    });
  }

  eliminar(material: MaterialDto) {
    this.materialClient.delete(material.id as any)
      .subscribe({
        next: res => {
          this.toastService.success(`Se eliminó la planta No.${material.id}`, {position: 'bottom-center'});
          this.cargarMateriales(this.materiales?.pageSize, this.materiales.pageNumber, this.selectedTipoMaterial?.id);
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
