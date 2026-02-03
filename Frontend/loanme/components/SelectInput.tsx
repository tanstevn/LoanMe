"use client";

import { InputHTMLAttributes, useEffect } from "react";

export interface SelectOption {
  id: string;
  value: string;
}

interface SelectInputProps extends InputHTMLAttributes<HTMLSelectElement> {
  label: string;
  id: string;
  options: SelectOption[] | undefined;
  isLoading: boolean;
  widthSize: "sm" | "md" | "lg" | "xl" | "2xl" | "full";
}

const SelectInput = ({
  label,
  id,
  options,
  isLoading,
  widthSize,
  ...props
}: SelectInputProps) => {
  useEffect(() => {
    if (isLoading === false && props.value === "") {
      props.onChange?.({ target: { value: options?.[0].id } } as any);
    }
  }, [props, options, isLoading]);

  const inputSize = (): string => {
    switch (widthSize) {
      case "sm":
        return "max-w-24";
      case "md":
        return "max-w-32";
      case "lg":
        return "max-w-40";
      case "xl":
        return "max-w-48";
      case "2xl":
        return "max-w-64";
      default:
        return "";
    }
  };

  return (
    <div className="sm:col-span-4">
      <label
        htmlFor={props.name}
        className="block text-sm font-medium leading-6 text-white"
      >
        {label}
      </label>

      <div className="mt-2">
        <select
          id={id}
          className={`block w-full ${inputSize()} rounded-md border-0 bg-white/5 pl-1 py-2.5 text-white shadow-sm ring-1 ring-inset ring-white/10 focus:ring-2 focus:ring-inset focus:ring-white sm:text-sm sm:leading-6 [&_*]:text-black ${isLoading && "cursor-not-allowed"}`}
          {...props}
        >
          {isLoading && <option>Loading...</option>}
          {!isLoading &&
            options?.map((option) => (
              <option key={option.id} value={option.id}>
                {option.value}
              </option>
            ))}
        </select>
      </div>
    </div>
  );
};

export default SelectInput;
