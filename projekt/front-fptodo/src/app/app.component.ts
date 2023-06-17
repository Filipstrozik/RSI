import { Component, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { TodoapiService } from './services/todoapi.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'front-fptodo';

  @ViewChild(MatSidenav) sidenav!: MatSidenav;

  authors: string[] = [];

  constructor(private todoApiService: TodoapiService) {
    this.todoApiService.getAuthors().subscribe((authors) => {
      this.authors = authors;
    });
  }

  toggleSidenav(): void {
    this.sidenav.toggle();
  }
}
