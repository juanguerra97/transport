import {Component, OnInit} from '@angular/core';
import {PlantaDto, PlantasClient, TipoPlantaClient, TipoPlantaDto} from "../../../web-api-client";
import {ConfirmationService} from "primeng/api";
import {HotToastService} from "@ngneat/hot-toast";
import {getErrorMessage} from "../../../utils/errors";

@Component({
  selector: 'app-home-plantas',
  templateUrl: './home-plantas.component.html',
  styleUrls: ['./home-plantas.component.scss']
})
export class HomePlantasComponent implements OnInit {

  plantas: PlantaDto[] = [];
  loadingPlantas = true;

  tiposPlanta: TipoPlantaDto[] = [];

  constructor(
    private plantasClient: PlantasClient,
    private tipoPlantaClient: TipoPlantaClient,
    private confirmationService: ConfirmationService,
    private toastService: HotToastService,
  ) { }

  ngOnInit(): void {
    this.cargarTiposPlanta();
    this.cargarPlantas();
  }

  private cargarTiposPlanta() {
    this.tipoPlantaClient.getTipoPlantas()
      .subscribe({
        next: res => {
          this.tiposPlanta = res;
        },
        error: error => {
          console.error(error);
          this.toastService.error(getErrorMessage(error), {dismissible: true, duration: 15000, position: 'bottom-center'});
        }
      });
  }

  private cargarPlantas() {
    this.loadingPlantas = true;
    this.plantasClient.getPlantas()
      .subscribe({
        next: res => {
          this.plantas = res;
          this.loadingPlantas = false;
        },
        error: error => {
          this.loadingPlantas = false;
          console.error(error);
          this.toastService.error(getErrorMessage(error), {dismissible: true, duration: 15000, position: 'bottom-center'});
        }
      })
  }

  confirmarEliminar(planta: PlantaDto) {
    this.confirmationService.confirm({
      message: `¿Seguro que quieres eliminar la planta No.${planta.id}?`,
      acceptLabel: 'Sí, eliminar',
      rejectLabel: 'Cancelar',
      acceptButtonStyleClass: 'p-button-sm p-button-raised p-button-danger',
      rejectButtonStyleClass: 'p-button-sm p-button-raised',
      accept: () => {
        this.eliminar(planta);
      }
    });
  }

  eliminar(planta: PlantaDto) {
    this.plantasClient.delete(planta.id as any)
      .subscribe({
        next: res => {
          this.toastService.success(`Se eliminó la planta No.${planta.id}`, {position: 'bottom-center'});
          this.cargarPlantas();
        },
        error: error => {
          console.error(error);
          this.toastService.error(getErrorMessage(error), {dismissible: true, duration: 15000, position: 'bottom-center'});
        }
      });
  }

}
