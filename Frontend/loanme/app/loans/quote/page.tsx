"use client";

import Button from "@/components/Button";
import { Card } from "@/components/Card";
import { toMoneyFormat } from "@/helpers/formatter";
import { useApiQuery } from "@/hooks/query";
import { useRouter, useSearchParams } from "next/navigation";

interface CalculateQuoteRequest {
  productId: number;
  draftLoanId: number;
}

interface CalculateQuoteResult {
  fullName: string;
  mobileNumber: string;
  emailAddress: string;
  loanAmount: number;
  loanTerm: number;
  totalInterest: number;
  establishmentFee: number;
  repaymentAmount: number;
}

const QuotePage = () => {
  const router = useRouter();
  const searchParams = useSearchParams();
  const productId = searchParams.get("productId");
  const draftLoanId = searchParams.get("draftLoanId");

  const { data: calculatedQuote, isLoading: isCalculatedQuoteLoading } =
    useApiQuery<CalculateQuoteResult>("/loan/calculator/quote", {
      productId,
      draftLoanId,
    });

  const totalRepayments = () => {
    const loanAmount = calculatedQuote?.loanAmount ?? 0;
    const totalInterest = calculatedQuote?.totalInterest ?? 0;
    const establishmentFee = calculatedQuote?.establishmentFee ?? 0;

    return toMoneyFormat(loanAmount + totalInterest + establishmentFee);
  };

  return (
    <Card>
      <Card.Title>Your Quote</Card.Title>
      <Card.Body>
        <div className="mt-6 flex justify-between">
          <p className="text-lg font-medium">Your information</p>
          <Button
            type="button"
            onClick={() => {
              router.replace(`/loans?id=${draftLoanId}`);
            }}
          >
            Edit
          </Button>
        </div>

        <div className="mt-5 flex flex-col gap-4">
          <div className="flex justify-between">
            <p className="text-base font-medium text-gray-400">Name</p>
            <p className="text-base font-medium text-gray-400">
              {calculatedQuote?.fullName}
            </p>
          </div>

          <div className="flex justify-between">
            <p className="text-base font-medium text-gray-400">Mobile</p>
            <p className="text-base font-medium text-gray-400">
              {calculatedQuote?.mobileNumber}
            </p>
          </div>

          <div className="flex justify-between">
            <p className="text-base font-medium text-gray-400">Email</p>
            <p className="text-base font-medium text-gray-400">
              {calculatedQuote?.emailAddress}
            </p>
          </div>
        </div>

        <div className="mt-14">
          <p className="text-lg font-medium">Finance details</p>
        </div>

        <div className="mt-5 flex flex-col gap-4">
          <div>
            <div className="flex justify-between">
              <p className="text-base font-medium text-gray-400">
                Finance amount
              </p>
              <p className="text-base font-medium text-gray-400">
                ${toMoneyFormat(calculatedQuote?.loanAmount)}
              </p>
            </div>

            <div className="flex items-center">
              <div className="flex-1 border-b border-dotted mx-2" />
              <p className="text-xs font-medium text-gray-400">
                for {calculatedQuote?.loanTerm} months
              </p>
            </div>
          </div>

          <div>
            <div className="flex justify-between">
              <p className="text-base font-medium text-gray-400">
                Repayment amount
              </p>
              <p className="text-base font-medium text-gray-400">
                ${toMoneyFormat(calculatedQuote?.repaymentAmount)}
              </p>
            </div>

            <div className="flex items-center">
              <div className="flex-1 border-b border-dotted mx-2" />
              <p className="text-xs font-medium text-gray-400">monthly</p>
            </div>
          </div>
        </div>

        <div className="mt-14 flex flex-col justify-center items-center gap-y-4">
          <Button type="button" className="w-64 flex-1">
            Apply now
          </Button>

          <p className="flex-2 mx-auto max-w-2xl text-center text-xs sm:text-xs text-gray-400 px-4">
            Total repayments ${totalRepayments()}, made up of an establishment
            fee of ${toMoneyFormat(calculatedQuote?.establishmentFee)}
            {!calculatedQuote?.totalInterest
              ? " with interest-free. "
              : `, interest
            of $${toMoneyFormat(calculatedQuote?.totalInterest)}. `}
            The repayment amount is based on the variables selected, is subject
            to our assessment and suitability, and other important terms and
            conditions apply.
          </p>
        </div>
      </Card.Body>
    </Card>
  );
};

export default QuotePage;
