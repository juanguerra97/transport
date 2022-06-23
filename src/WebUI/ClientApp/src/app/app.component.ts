import {Component} from '@angular/core';
import {INavData} from "@coreui/angular";
import {MenuService} from "./services/menu.service";
import {Title} from "@angular/platform-browser";
import {PrimeNGConfig} from "primeng/api";
import {IdiomaEs} from "./core/idioma-es";

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
        {name: 'Plantas', url: '/admin/catalogo/plantas'},
        {name: 'Bodegas', url: '/admin/catalogo/bodegas'},
        {name: 'Materiales', url: '/admin/catalogo/materiales'},
        {name: 'Medidas', url: '/admin/catalogo/medidas'}
      ]
    }
  ];
  title = 'app';

  public perfectScrollbarConfig = {
    suppressScrollX: true,
  };

  constructor(
    public menuService: MenuService,
    private titleService: Title,
    private config: PrimeNGConfig,
  ) {
    this.titleService.setTitle('TransPort');
    this.config = IdiomaEs.idiomaEs(this.config);
  }

}
