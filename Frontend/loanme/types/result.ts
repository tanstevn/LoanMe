export type ResultErrors = string[];

export interface UnitResult {
  successful: boolean;
  errors: ResultErrors;
}

export interface Result<T> extends UnitResult {
  data: T;
}
