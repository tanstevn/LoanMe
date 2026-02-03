"use client";

import { InputHTMLAttributes, useEffect } from "react";

export interface SelectOpntion {
  id: string;
  value: string;
}

interface SelectInputProps extends InputHTMLAttributes<HTMLSelectElement> {
  label: string;
  id: string;
  options: SelectOpntion[] | undefined;
  isLoading: boolean;
}

const SelectInput = ({
  label,
  id,
  options,
  isLoading,
  ...props
}: SelectInputProps) => {
  useEffect(() => {
    if (isLoading === false && props.value === "") {
      props.onChange?.({ target: { value: options?.[0].id } } as any);
    }
  }, [props, options, isLoading]);

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
          className={`block w-full rounded-md border-0 bg-white/5 pl-1 py-2.5 text-white shadow-sm ring-1 ring-inset ring-white/10 focus:ring-2 focus:ring-inset focus:ring-white sm:text-sm sm:leading-6 [&_*]:text-black ${isLoading && "cursor-not-allowed"}`}
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
