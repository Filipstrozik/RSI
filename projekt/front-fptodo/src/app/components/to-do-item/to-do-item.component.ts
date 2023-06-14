import { Component, Input } from '@angular/core';
import ToDoItem from 'src/app/models/todoitem';

@Component({
  selector: 'app-to-do-item',
  templateUrl: './to-do-item.component.html',
  styleUrls: ['./to-do-item.component.css']
})
export class ToDoItemComponent {
  @Input() item!: ToDoItem;
}
