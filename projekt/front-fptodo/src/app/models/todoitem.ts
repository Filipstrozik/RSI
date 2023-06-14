import Board from './board';
import User from './user';

export interface ToDoItem {
  id: number;
  name?: string;
  isComplete: boolean;
  dueTime: Date;
  priority: number;
  board: Board;
  user?: User;
}

export default ToDoItem;
