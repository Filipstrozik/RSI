import { Component, Input } from '@angular/core';
import Board from 'src/app/models/board';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css']
})
export class BoardComponent {
  @Input() board!: Board;
}
