import { AbstractControl, ValidationErrors } from '@angular/forms';

export function dateRangeValidator(
  startControlName: string,
  endControlName: string
) {
  return (control: AbstractControl): ValidationErrors | null => {

    const start = control.get(startControlName)?.value;
    const end = control.get(endControlName)?.value;

    if (!start || !end) return null;

    const startDate = new Date(start);
    const endDate = new Date(end);

    return endDate >= startDate
      ? null
      : { dateRangeInvalid: true };
  };
}
