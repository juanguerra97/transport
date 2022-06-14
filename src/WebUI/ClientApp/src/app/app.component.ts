import { Component } from '@angular/core';
import {INavData} from "@coreui/angular";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {

  navItems: INavData[] = [
    {
      title: true,
      name: 'Administración'
    },
    {
      name: 'Catálogos',
      icon: 'fa fa-solid fa-table',
      children: [
        { name: 'Plantas', url: '/admin/catalogo/plantas' },
        { name: 'Bodegas', url: '/admin/catalogo/bodegas' },
        { name: 'Materiales', url: '/admin/catalogo/materiales'},
        { name: 'Medidas', url: '/admin/catalogo/medidas'}
      ]
    }
  ];
  title = 'app';

  public perfectScrollbarConfig = {
    suppressScrollX: true,
  };
}
