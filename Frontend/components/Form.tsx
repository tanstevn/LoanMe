"use client";

import { HTMLAttributes, PropsWithChildren } from "react";

interface FormProps extends PropsWithChildren<
  HTMLAttributes<HTMLFormElement>
> {}

interface FormHeaderProps extends HTMLAttributes<HTMLDivElement> {
  title: string;
  description: string;
}

interface FormSectionProps extends PropsWithChildren<
  HTMLAttributes<HTMLDivElement>
> {
  className?: string;
}

const FormRoot = ({ children, ...props }: FormProps) => {
  return <form {...props}>{children}</form>;
};

const FormHeader = ({ title, description, ...props }: FormHeaderProps) => {
  return (
    <div {...props}>
      <h2 className="text-base font-semibold leading-7 text-white">{title}</h2>
      <p className="mt-1 text-sm leading-6 text-gray-400">{description}</p>
    </div>
  );
};

const FormSection = ({ className, children, ...props }: FormSectionProps) => {
  return (
    <div className="pb-6" {...props}>
      <div className={`${className} mt-10 gap-x-6 gap-y-8 sm:grid-cols-6`}>
        {children}
      </div>
    </div>
  );
};

export let Form = Object.assign(FormRoot, {
  Header: FormHeader,
  Section: FormSection,
});
