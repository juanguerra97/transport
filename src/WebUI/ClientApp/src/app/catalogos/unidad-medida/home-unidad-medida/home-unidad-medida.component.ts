import {ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {UnidadMedidaClient, UnidadMedidaDto} from "../../../web-api-client";
import {getErrorMessage} from "../../../utils/errors";
import {HotToastService} from "@ngneat/hot-toast";
import {ConfirmationService} from "primeng/api";
import {DialogService} from "primeng/dynamicdialog";
import {NewUnidadMedidaComponent} from "../new-unidad-medida/new-unidad-medida.component";
import {EditUnidadMedidaComponent} from "../edit-unidad-medida/edit-unidad-medida.component";

@Component({
  selector: 'app-home-unidad-medida',
  templateUrl: './home-unidad-medida.component.html',
  styleUrls: ['./home-unidad-medida.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HomeUnidadMedidaComponent implements OnInit {

  medidas: UnidadMedidaDto[] = [];
  loadingMedidas = true;

  modalNuevoAbierto = false;
  modalEditarAbierto = false;

  constructor(
    private unidadMedidaClient: UnidadMedidaClient,
    private confirmationClient: ConfirmationService,
    private dialogService: DialogService,
    private toastService: HotToastService,
    private cdr: ChangeDetectorRef,
  ) {
  }

  ngOnInit(): void {
    this.cargarMedidas();
  }

  private cargarMedidas() {
    this.loadingMedidas = true;
    this.cdr.markForCheck();
    this.unidadMedidaClient.getUnidadesMedida()
      .subscribe({
        next: res => {
          this.medidas = res;
          this.loadingMedidas = false;
          this.cdr.markForCheck();
        },
        error: error => {
          console.error(error);
          this.loadingMedidas = false;
          this.cdr.markForCheck();
          this.toastService.error(getErrorMessage(error), {
            dismissible: true,
            duration: 15000,
            position: 'bottom-center'
          });
        }
      });
  }

  openModalNuevo() {
    if (this.modalNuevoAbierto) {
      return;
    }
    this.modalNuevoAbierto = true;
    const ref = this.dialogService.open(NewUnidadMedidaComponent, {
      header: 'Nueva',
      styleClass: 'modal-catalogo'
    });
    ref.onClose.subscribe({
      next: res => {
        this.modalNuevoAbierto = false;
        if (res) {
          this.cargarMedidas();
        }
      }
    });
  }

  openModalEditar(medida: UnidadMedidaDto) {
    if (this.modalEditarAbierto) {
      return;
    }
    this.modalEditarAbierto = true;
    const ref = this.dialogService.open(EditUnidadMedidaComponent, {
      header: 'Editar',
      styleClass: 'modal-catalogo',
      data: medida
    });
    ref.onClose.subscribe({
      next: res => {
        this.modalEditarAbierto = false;
        this.cargarMedidas();
      }
    });
  }

  confirmarEliminar(medida: UnidadMedidaDto) {
    this.confirmationClient.confirm({
      message: `¿Seguro que quieres eliminar la unidad de medida No.${medida.id}?`,
      acceptLabel: 'Sí, eliminar',
      rejectLabel: 'Cancelar',
      acceptButtonStyleClass: 'p-button-sm p-button-raised p-button-danger',
      rejectButtonStyleClass: 'p-button-sm p-button-raised',
      accept: () => {
        this.eliminar(medida);
      }
    });
  }

  eliminar(medida: UnidadMedidaDto) {
    this.unidadMedidaClient.delete(medida.id as any)
      .subscribe({
        next: res => {
          this.toastService.success(`Se eliminó la unidad de medida No.${medida.id}`, {position: 'bottom-center'});
          this.cargarMedidas();
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
