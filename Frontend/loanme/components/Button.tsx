"use client";

import { HTMLAttributes } from "react";

interface ButtonProps extends HTMLAttributes<HTMLButtonElement> {
  className?: string;
  type?: "button" | "submit";
  disabled?: boolean;
}

const Button = ({
  children,
  className,
  disabled,
  type,
  ...props
}: ButtonProps) => {
  return (
    <button
      className={`${className} block rounded-md bg-black border border-gray-500 hover:border-gray-200 transition-all px-3 py-2 text-center text-sm font-semibold text-white focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-white`}
      type={type}
      disabled={disabled}
      {...props}
    >
      {children}
    </button>
  );
};

export default Button;
