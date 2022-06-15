import {NgModule} from '@angular/core';
import {TableModule} from 'primeng/table';
import {DropdownModule} from 'primeng/dropdown';
import {ButtonModule} from 'primeng/button';
import {InputTextareaModule} from 'primeng/inputtextarea';
import {ConfirmDialogModule} from 'primeng/confirmdialog';
import {ConfirmationService} from 'primeng/api';
import {ProgressSpinnerModule} from 'primeng/progressspinner';
import {PaginatorModule} from 'primeng/paginator';
import {InputNumberModule} from 'primeng/inputnumber';
import {KeyFilterModule} from 'primeng/keyfilter';
import {DialogService, DynamicDialogModule} from 'primeng/dynamicdialog';
import {AutoCompleteModule} from 'primeng/autocomplete';
import {CardModule} from 'primeng/card';

@NgModule({
  declarations: [],
  imports: [],
  exports: [
    TableModule,
    DropdownModule,
    ButtonModule,
    InputTextareaModule,
    ConfirmDialogModule,
    ProgressSpinnerModule,
    PaginatorModule,
    InputNumberModule,
    KeyFilterModule,
    DynamicDialogModule,
    AutoCompleteModule,
    CardModule,
  ],
  providers: [
    ConfirmationService,
    DialogService,
  ]
})
export class PrimeNgModule {
}
