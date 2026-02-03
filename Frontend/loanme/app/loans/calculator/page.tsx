"use client";

import Button from "@/components/Button";
import { Card } from "@/components/Card";
import DateInput from "@/components/DateInput";
import { Form } from "@/components/Form";
import SelectInput, { SelectOption } from "@/components/SelectInput";
import Slider from "@/components/Slider";
import TextInput from "@/components/TextInput";
import { useApiQuery } from "@/hooks/query";
import { useSearchParams } from "next/navigation";
import { useEffect, useState } from "react";

interface ProductsList {
  items: SelectOption[];
}

interface TitlesList {
  items: SelectOption[];
}

interface GetAllProductsResult {
  id: number;
  name: string;
  description: string;
  minimumTerm: number;
  maximumTerm: number;
  minLoanAmount: number;
  maxLoanAmount: number;
}

interface GetDraftLoanResult {
  title: string;
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  mobileNumber: string;
  emailAddress: string;
  term: number;
  loanAmount: number;
}

const LoansCalculator = () => {
  const [titleId, setTitleId] = useState(1);
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [dateOfBirth, setDateOfBirth] = useState("");
  const [mobileNumber, setMobileNumber] = useState("");
  const [emailAddress, setEmailAddress] = useState("");
  const [term, setTerm] = useState(0);
  const [loanAmount, setLoanAmount] = useState(0);
  const [selectedProductId, setSelectedProductId] = useState("");
  const [products, setProducts] = useState<GetAllProductsResult[]>();
  const [productsList, setProductsList] = useState<ProductsList>();

  const searchParams = useSearchParams();
  const id = searchParams.get("id");

  const { data: draftedLoan, isLoading: isGetDraftLoanLoading } =
    useApiQuery<GetDraftLoanResult>("/loan/draft", {
      id,
    });

  const { data: allProducts, isLoading: isGetAllProductsLoading } =
    useApiQuery<GetAllProductsResult[]>("/product/all");

  const titlesList: TitlesList = {
    items: [
      { id: "1", value: "Mr." },
      { id: "2", value: "Ms." },
    ],
  };

  const selectedProduct = products?.find(
    (product) => product.id.toString() === selectedProductId,
  );

  const minLoanAmount = selectedProduct?.minLoanAmount ?? 2000;
  const maxLoanAmount = selectedProduct?.maxLoanAmount ?? 5000;

  const minLoanTerm = selectedProduct?.minimumTerm ?? 1;
  const maxLoanTerm = selectedProduct?.maximumTerm ?? 12;

  const toDateString = (dateTime?: string) => {
    return dateTime?.split("T")[0];
  };

  const onSubmit = () => {};

  useEffect(() => {
    if (draftedLoan) {
      const titleId = draftedLoan.title === "Mr." ? 1 : 2;

      setTitleId(titleId);
      setFirstName(draftedLoan.firstName);
      setLastName(draftedLoan.lastName);
      setDateOfBirth(toDateString(draftedLoan.dateOfBirth) ?? "");
      setMobileNumber(draftedLoan.mobileNumber);
      setEmailAddress(draftedLoan.emailAddress);
      setTerm(draftedLoan.term);
      setLoanAmount(draftedLoan.loanAmount);
    }
  }, [draftedLoan]);

  useEffect(() => {
    if (allProducts) {
      const products: ProductsList = {
        items: allProducts.map((product) => ({
          id: product.id.toString(),
          value: product.name,
          description: product.description,
        })),
      };

      setProductsList(products);
      setProducts(allProducts);
      setSelectedProductId(products.items[0].id);
    }
  }, [allProducts]);

  useEffect(() => {
    setLoanAmount((previous) => {
      if (previous < minLoanAmount) {
        return minLoanAmount;
      } else if (previous > maxLoanAmount) {
        return maxLoanAmount;
      }

      return previous;
    });
  }, [selectedProduct, minLoanAmount, maxLoanAmount]);

  useEffect(() => {
    setTerm((previous) => {
      if (previous < minLoanTerm) {
        return minLoanTerm;
      } else if (previous > maxLoanTerm) {
        return maxLoanTerm;
      }

      return previous;
    });
  }, [selectedProduct, minLoanTerm, maxLoanTerm]);

  return (
    <>
      {!isGetDraftLoanLoading && !isGetAllProductsLoading && (
        <Form>
          <Card>
            <Card.Title>Quote Calculator</Card.Title>
            <Card.Body>
              <Form.Section className="space-y-10">
                <SelectInput
                  id="productId"
                  label="Products"
                  type="text"
                  name="productId"
                  autoComplete="off"
                  value={selectedProductId}
                  onChange={(event) => setSelectedProductId(event.target.value)}
                  options={productsList?.items}
                  isLoading={false}
                  widthSize="lg"
                />

                <Slider
                  id="loanAmount"
                  label="Loan amount"
                  message="How much do you need?"
                  minimum={minLoanAmount}
                  maximum={maxLoanAmount}
                  value={loanAmount}
                  unit="$"
                  onChange={(event) =>
                    setLoanAmount(Number(event.target.value))
                  }
                />

                <Slider
                  id="loanTerm"
                  label="Loan term (in months)"
                  message="How many months do you need?"
                  value={term}
                  minimum={minLoanTerm}
                  maximum={maxLoanTerm}
                  onChange={(event) => {
                    setTerm(Number(event.target.value));
                  }}
                />
              </Form.Section>

              <Form.Section>
                <div className="flex justify-center items-center space-x-8">
                  <SelectInput
                    id="title"
                    label="Title"
                    type="text"
                    name="titleId"
                    autoComplete="off"
                    value={titleId}
                    onChange={(event) => setTitleId(Number(event.target.value))}
                    options={titlesList?.items}
                    isLoading={false}
                    widthSize="lg"
                  />

                  <TextInput
                    id="firstName"
                    label="First name"
                    value={firstName}
                    onChange={(event) => setFirstName(event.target.value)}
                  />

                  <TextInput
                    id="lastName"
                    label="Last name"
                    value={lastName}
                    onChange={(event) => setLastName(event.target.value)}
                  />

                  <DateInput
                    id="dateOfBirth"
                    label="Date of birth"
                    value={dateOfBirth}
                    onChange={(event) => setDateOfBirth(event.target.value)}
                  />
                </div>

                <div className="flex justify-center items-center space-x-8 mt-4">
                  <TextInput
                    id="mobileNumber"
                    label="Mobile number"
                    value={mobileNumber}
                    onChange={(event) => setMobileNumber(event.target.value)}
                  />

                  <TextInput
                    id="emailAddress"
                    label="Email address"
                    value={emailAddress}
                    onChange={(event) => setEmailAddress(event.target.value)}
                  />
                </div>
              </Form.Section>

              <Form.Section>
                <div className="flex flex-col justify-center items-center gap-y-4">
                  <Button type="submit" className="w-80 flex-1">
                    Calculate quote
                  </Button>

                  <p className="flex-2 text-xs leading-6 text-gray-400">
                    Quote does not affect your credit score
                  </p>
                </div>
              </Form.Section>
            </Card.Body>
          </Card>
        </Form>
      )}
    </>
  );
};

export default LoansCalculator;
