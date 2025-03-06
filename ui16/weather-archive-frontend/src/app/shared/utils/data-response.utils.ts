import { DataResponse } from "../interfaces/data-response.model";

export function isValid<T>(response: DataResponse<T>): boolean {
  return !response.errors || response.errors.length === 0;
}
