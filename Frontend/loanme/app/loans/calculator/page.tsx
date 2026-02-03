"use client";

import Button from "@/components/Button";
import { Card } from "@/components/Card";
import DateInput from "@/components/DateInput";
import { Form } from "@/components/Form";
import SelectInput, { SelectOption } from "@/components/SelectInput";
import Slider from "@/components/Slider";
import TextInput from "@/components/TextInput";
import { useState } from "react";

interface ProductsList {
  items: SelectOption[];
}

interface TitlesList {
  items: SelectOption[];
}

const LoansCalculator = () => {
  const [productId, setProductId] = useState("");
  const [titleId, setTitleId] = useState("");

  const productsList: ProductsList = {
    items: [
      { id: "1", value: "House" },
      { id: "2", value: "Car" },
      { id: "3", value: "Personal" },
    ],
  };

  const titlesList: TitlesList = {
    items: [
      { id: "1", value: "Mr." },
      { id: "2", value: "Ms." },
    ],
  };

  const onSubmit = () => {};

  return (
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
              value={productId}
              onChange={(event) => setProductId(event.target.value)}
              options={productsList?.items}
              isLoading={false}
              widthSize="lg"
            />

            <Slider
              id="loanAmountId"
              label="Loan amount"
              message="How much do you need?"
              current={5000}
              minimum={2100}
              maximum={5000}
              unit="$"
            />

            <Slider
              id="loanTermId"
              label="Loan term (in months)"
              message="How much do you need?"
              current={2}
              minimum={1}
              maximum={24}
            />
          </Form.Section>

          <Form.Section>
            <div className="flex justify-center items-center space-x-8">
              <SelectInput
                id="titleId"
                label="Title"
                type="text"
                name="titleId"
                autoComplete="off"
                value={titleId}
                onChange={(event) => setTitleId(event.target.value)}
                options={titlesList?.items}
                isLoading={false}
                widthSize="lg"
              />

              <TextInput id="firstName" label="First name" widthSize="3xl" />
              <TextInput id="lastName" label="Last name" widthSize="3xl" />
              <DateInput id="dateOfBirth" label="Date of birth" />
            </div>

            <div className="flex justify-center items-center space-x-8 mt-4">
              <TextInput
                id="mobileNumber"
                label="Mobile number"
                widthSize="3xl"
              />

              <TextInput
                id="emailAddress"
                label="Email address"
                widthSize="3xl"
              />
            </div>
          </Form.Section>

          <Form.Section>
            <div className="flex justify-center">
              <Button type="button">Calculate quote</Button>
            </div>
          </Form.Section>
        </Card.Body>
      </Card>
    </Form>
  );
};

export default LoansCalculator;
