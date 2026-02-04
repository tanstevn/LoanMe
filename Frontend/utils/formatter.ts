export const toMoneyFormat = (value?: number) => {
  if (!value) {
    return "";
  }

  const formatter = new Intl.NumberFormat("en-US", {
    style: "decimal",
    minimumFractionDigits: 0,
    maximumFractionDigits: 2,
  });

  return formatter.format(value!);
};
