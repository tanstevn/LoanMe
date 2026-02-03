"use client";

import { InputHTMLAttributes } from "react";

interface TextInputProps extends InputHTMLAttributes<HTMLInputElement> {
  label: string;
  id: string;
  placeholder?: string;
  widthSize: "sm" | "md" | "lg" | "xl" | "2xl" | "3xl" | "4xl" | "full";
}

const TextInput = ({
  label,
  id,
  placeholder,
  widthSize,
  ...props
}: TextInputProps) => {
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
      case "3xl":
        return "max-w-80";
      case "4xl":
        return "max-w-96";
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
        <div
          className={`flex w-full ${inputSize()} rounded-md bg-white/5 ring-1 ring-inset ring-white/10 focus-within:ring-2 focus_within:ring-inset`}
        >
          <input
            id={id}
            placeholder={placeholder}
            className="flex-1 border-0 bg-transparent py-1.5 pl-1 text-white focus:ring-0 sm:text-sm sm:leading-6"
            {...props}
          />
        </div>
      </div>
    </div>
  );
};

export default TextInput;
