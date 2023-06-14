import ToDoItem from "./todoitem";

export interface Board {
  id: number;
  name: string;
  description: string;
  dueTime: Date;
  toDoItems: ToDoItem[];
}

export default Board;
