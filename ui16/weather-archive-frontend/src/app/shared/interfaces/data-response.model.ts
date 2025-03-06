import { IDataError } from './data-error.model';

export interface DataResponse<T> {
  data: T | null;
  errors: IDataError[];
}
