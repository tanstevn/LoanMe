"use client";

import { useSearchParams } from "next/navigation";
import LoansCalculator from "./calculator/page";

const LoansPage = () => {
  const searchParams = useSearchParams();
  const draftLoanId = searchParams.get("id");

  return (
    <main>
      {!draftLoanId ? (
        <div className="w-full lg:w-1/2 mx-auto">
          <h1 className="text-6xl py-6 text-center tracking-tighter font-mono text-transparent bg-clip-text bg-gradient-to-r from-sky-400 via-blue-500 to-indigo-600">
            Welcome to Loans
          </h1>
        </div>
      ) : (
        <LoansCalculator id={draftLoanId} />
      )}
    </main>
  );
};

export default LoansPage;
