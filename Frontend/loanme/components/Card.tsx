"use client";

import { HTMLAttributes, PropsWithChildren } from "react";

interface CardProps extends PropsWithChildren<HTMLAttributes<HTMLDivElement>> {
  className?: string;
}

interface CardTitleProps extends PropsWithChildren<
  HTMLAttributes<HTMLElement>
> {
  className?: string;
}

interface CardBodyProps extends PropsWithChildren<
  HTMLAttributes<HTMLParagraphElement>
> {
  className?: string;
}

const BaseCard = ({ children, className, ...props }: CardProps) => {
  return (
    <div
      className={`${className} flex flex-col space-y-0.5 py-7 px-12 border rounded-lg bg-gradient-to-br from-gray-950 to-black border-gray-800`}
      {...props}
    >
      {children}
    </div>
  );
};

const CardTitle = ({ children, className, ...props }: CardTitleProps) => {
  return (
    <h1 className={`${className} text-4xl font-medium mb-6`} {...props}>
      {children}
    </h1>
  );
};

const CardBody = ({ children, className, ...props }: CardBodyProps) => {
  return (
    <p className={`${className} text-xs text-gray-400, pl-0.5`} {...props}>
      {children}
    </p>
  );
};

export let Card = Object.assign(BaseCard, {
  Title: CardTitle,
  Body: CardBody,
});
