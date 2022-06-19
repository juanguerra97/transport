import {NgModule} from '@angular/core';
import {RouterModule, Routes} from "@angular/router";
import {HomeVehiculosComponent} from './home-vehiculos/home-vehiculos.component';
import { NewVehiculoComponent } from './new-vehiculo/new-vehiculo.component';
import { EditVehiculoComponent } from './edit-vehiculo/edit-vehiculo.component';
import { FormVehiculoComponent } from './form-vehiculo/form-vehiculo.component';
import {CoreModule} from "../../core/core.module";

const routes: Routes = [
  {path: '', pathMatch: 'full', component: HomeVehiculosComponent},
];

@NgModule({
  declarations: [
    HomeVehiculosComponent,
    NewVehiculoComponent,
    EditVehiculoComponent,
    FormVehiculoComponent
  ],
  imports: [
    CoreModule,
    RouterModule.forChild(routes)
  ]
})
export class VehiculosModule {
}
