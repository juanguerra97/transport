import {NgModule} from '@angular/core';
import {CoreModule} from "../../core/core.module";
import {RouterModule, Routes} from "@angular/router";
import {HomeUnidadMedidaComponent} from './home-unidad-medida/home-unidad-medida.component';
import {NewUnidadMedidaComponent} from './new-unidad-medida/new-unidad-medida.component';
import { FormUnidadMedidaComponent } from './form-unidad-medida/form-unidad-medida.component';
import { EditUnidadMedidaComponent } from './edit-unidad-medida/edit-unidad-medida.component';

const routes: Routes = [
  {path: '', pathMatch: 'full', component: HomeUnidadMedidaComponent},
];

@NgModule({
  declarations: [
    HomeUnidadMedidaComponent,
    NewUnidadMedidaComponent,
    FormUnidadMedidaComponent,
    EditUnidadMedidaComponent,
  ],
  imports: [
    CoreModule,
    RouterModule.forChild(routes),
  ]
})
export class UnidadMedidaModule {
}
