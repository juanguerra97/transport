import {Component, OnInit} from '@angular/core';
import {AuthorizeService} from "../../api-authorization/authorize.service";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  isAuthenticated = false;

  constructor(public authService: AuthorizeService) {

  }

  ngOnInit(): void {
    this.authService.isAuthenticated()
      .subscribe({
        next: res => {
          this.isAuthenticated = res;
        }
      });
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
