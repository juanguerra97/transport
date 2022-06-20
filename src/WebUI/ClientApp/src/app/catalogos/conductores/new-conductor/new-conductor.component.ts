import {Component, OnInit} from '@angular/core';
import {ConductorDto, ConductoresClient} from "../../../web-api-client";
import {DynamicDialogConfig, DynamicDialogRef} from "primeng/dynamicdialog";
import {HotToastService} from "@ngneat/hot-toast";
import {getErrorMessage} from "../../../utils/errors";

@Component({
  selector: 'app-new-conductor',
  templateUrl: './new-conductor.component.html',
  styleUrls: ['./new-conductor.component.scss']
})
export class NewConductorComponent implements OnInit {

  guardando = false;

  constructor(
    private conductoresClient: ConductoresClient,
    private ref: DynamicDialogRef,
    private config: DynamicDialogConfig,
    private toastService: HotToastService,
  ) {
  }

  ngOnInit(): void {
  }

  guardar(conductor: ConductorDto) {
    this.guardando = true;
    this.conductoresClient.create(conductor).subscribe({
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
