import {ChangeDetectionStrategy, Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {UnidadMedidaDto} from "../../../web-api-client";

@Component({
  selector: 'app-form-unidad-medida',
  templateUrl: './form-unidad-medida.component.html',
  styleUrls: ['./form-unidad-medida.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class FormUnidadMedidaComponent implements OnInit {

  _medida?: UnidadMedidaDto;
  @Input() set medida(value: UnidadMedidaDto|undefined) {
    this._medida = value;
    if (value) {
      this.form.patchValue(value);
    } else {
      this.form.reset();
    }
  }
  @Input() guardando = false;

  @Output() onGuardar = new EventEmitter<UnidadMedidaDto>();
  @Output() onCancelar = new EventEmitter<null>();

  form = new FormGroup({
    descripcion: new FormControl(null, [Validators.required, Validators.maxLength(128)]),
    descripcionCorta: new FormControl(null, [Validators.required, Validators.maxLength(128)]),
    descripcionPlural: new FormControl(null, [Validators.required, Validators.maxLength(128)]),
  });

  constructor() { }

  get descripcion() {
    return this.form.get('descripcion');
  }

  get descripcionCorta() {
    return this.form.get('descripcionCorta');
  }

  get descripcionPlural() {
    return this.form.get('descripcionPlural');
  }

  ngOnInit(): void {
  }

  guardar() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    this.onGuardar.emit(this.form.value);
  }

  cancelar() {
    this.onCancelar.emit(null);
  }

}
