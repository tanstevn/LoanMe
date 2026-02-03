import { InputHTMLAttributes, useState } from "react";

interface SliderProps extends InputHTMLAttributes<HTMLInputElement> {
  label: string;
  id: string;
  message: string;
  current: number;
  minimum: number;
  maximum: number;
  unit?: string;
}

const Slider = ({
  label,
  id,
  message,
  current,
  minimum,
  maximum,
  unit,
  ...props
}: SliderProps) => {
  const [curr, setCurr] = useState(current);

  const percentage = ((curr - minimum) / (maximum - minimum)) * 100;

  return (
    <div className="relative mb-6">
      <label
        htmlFor={props.name}
        className="block text-sm font-medium leading-6 text-white"
      >
        {label}
      </label>

      <span
        className="absolute -top-2 text-sm font-medium text-body"
        style={{
          left: `calc(${percentage}% )`,
          transform: "translateX(-50%)",
        }}
      >
        {unit}
        {curr}
      </span>

      <input
        data-tooltip-target="tooltip-default"
        id={id}
        type="range"
        min={minimum}
        max={maximum}
        value={curr}
        onChange={(event) => setCurr(Number(event.target.value))}
        className="w-full h-2 bg-neutral-quaternary rounded-full appearance-none cursor-pointer"
        {...props}
      />

      <span className="text-sm text-body absolute start-0 -bottom-6">
        {unit}
        {minimum}
      </span>

      <span className="text-sm text-body absolute start-1/2 -translate-x-1/2 rtl:translate-x-1/2 -bottom-6">
        {message}
      </span>

      <span className="text-sm text-body absolute end-0 -bottom-6">
        {unit}
        {maximum}
      </span>
    </div>
  );
};

export default Slider;
