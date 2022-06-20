import {Component, OnInit} from '@angular/core';
import {ConductorDto, ConductoresClient} from "../../../web-api-client";
import {DynamicDialogConfig, DynamicDialogRef} from "primeng/dynamicdialog";
import {HotToastService} from "@ngneat/hot-toast";
import {getErrorMessage} from "../../../utils/errors";

@Component({
  selector: 'app-edit-conductor',
  templateUrl: './edit-conductor.component.html',
  styleUrls: ['./edit-conductor.component.scss']
})
export class EditConductorComponent implements OnInit {

  conductor?: ConductorDto;
  guardando = false;

  constructor(
    private conductoresClient: ConductoresClient,
    private ref: DynamicDialogRef,
    private config: DynamicDialogConfig,
    private toastService: HotToastService,
  ) {
  }

  ngOnInit(): void {
    this.conductor = this.config.data;
  }

  guardar(conductor: ConductorDto) {
    this.guardando = true;
    conductor.id = this.conductor?.id;
    this.conductoresClient.update(conductor.id as any, conductor)
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
