import { Directive } from "@angular/core";
import { NG_VALIDATORS, FormControl, ValidatorFn, Validator } from "@angular/forms";

@Directive({
  selector: "[datevalidator]",
  providers: [
    {
      provide: NG_VALIDATORS,
      useExisting: DateValidatorDirective,
      multi: true,
    },
  ],
})
export class DateValidatorDirective implements Validator {
  validator: ValidatorFn;
  constructor() {
    this.validator = this.dateValidator();
  }
  validate(c: FormControl) {
    return this.validator(c);
  }

  dateValidator(): ValidatorFn {
    return (c: FormControl) => {
      let isValid = true;
      if (c.value) {
        let data = c.value.split("/");
        let d = new Date(data[2] + "/" + data[1] + "/" + data[0]);
        isValid =
          d &&
          d.getMonth() + 1 == data[1] &&
          d.getDate() == Number(data[0]) &&
          d.getFullYear() == Number(data[2]);
      }
      if (isValid) {
        return null;
      } else {
        return {
          datevalidator: {
            valid: false,
          },
        };
      }
    };
  }
}
