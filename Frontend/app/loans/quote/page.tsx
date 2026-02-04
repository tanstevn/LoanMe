"use client";

import Button from "@/components/Button";
import { Card } from "@/components/Card";
import { toMoneyFormat } from "@/utils/formatter";
import { useApiMutation, useApiQuery } from "@/hooks/query";
import { useRouter, useSearchParams } from "next/navigation";
import { useApplicationNumberContext } from "@/hooks/context";
import { toast } from "react-toastify";

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

interface ApplyLoanRequest {
  draftLoanId: number;
  productId: number;
  repaymentAmount: number;
  establishmentFee: number;
  totalInterest: number;
}

interface ApplyLoanResult {
  applicationNumber: string;
}

const QuotePage = () => {
  const router = useRouter();
  const searchParams = useSearchParams();
  const applicationNumberContext = useApplicationNumberContext();

  const productId = searchParams.get("productId");
  const draftLoanId = searchParams.get("id");

  const { data: calculatedQuote, isLoading: isCalculatedQuoteLoading } =
    useApiQuery<CalculateQuoteResult>("/loan/calculator/quote", {
      productId,
      draftLoanId,
    });

  const {
    mutateAsync: mutateApplyLoan,
    isIdle: isApplyLoanMutationIdle,
    reset: resetApplyLoan,
  } = useApiMutation<ApplyLoanRequest, ApplyLoanResult>("/loan", "POST", {
    onSuccess(result: ApplyLoanResult) {
      applicationNumberContext?.setApplicationNumber(result.applicationNumber);

      router.replace("/loans/success");
    },
    onError(error) {
      toast.error(error.join(", "));
      resetApplyLoan();
    },
  });

  const totalRepayments = () => {
    const loanAmount = calculatedQuote?.loanAmount ?? 0;
    const totalInterest = calculatedQuote?.totalInterest ?? 0;
    const establishmentFee = calculatedQuote?.establishmentFee ?? 0;

    return toMoneyFormat(loanAmount + totalInterest + establishmentFee);
  };

  const onSubmit = () => {
    if (isApplyLoanMutationIdle) {
      const request: ApplyLoanRequest = {
        draftLoanId: Number(draftLoanId),
        productId: Number(productId),
        repaymentAmount: calculatedQuote?.repaymentAmount ?? 0,
        establishmentFee: calculatedQuote?.establishmentFee ?? -1,
        totalInterest: calculatedQuote?.totalInterest ?? -1,
      };

      mutateApplyLoan(request);
    }
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
          <Button type="submit" className="w-64 flex-1" onClick={onSubmit}>
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
