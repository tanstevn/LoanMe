"use client";

import { Card } from "@/components/Card";
import { useApplicationNumberContext } from "@/hooks/context";

const SuccessPage = () => {
  const applicationNumberContext = useApplicationNumberContext();

  return (
    <>
      {!applicationNumberContext?.applicationNumber ? (
        <div className="w-full lg:w-1/2 mx-auto">
          <h1 className="text-6xl py-6 text-center tracking-tighter font-mono text-transparent bg-clip-text bg-gradient-to-r from-sky-400 via-blue-500 to-indigo-600">
            Success right?
          </h1>
        </div>
      ) : (
        <Card>
          <Card.Title>Application Submitted</Card.Title>
          <Card.Body>
            <div className="mt-2 flex flex-col gap-2">
              <p className="text-base font-medium">
                Your loan request has been submitted!
              </p>

              <p className="text-base font-medium mb-4">
                Please take note of your loan application number below for
                verification purposes:
              </p>

              <p className="text-2xl font-medium text-transparent bg-clip-text bg-gradient-to-r from-sky-400 via-blue-500 to-indigo-600">
                {applicationNumberContext?.applicationNumber}
              </p>
            </div>
          </Card.Body>
        </Card>
      )}
    </>
  );
};

export default SuccessPage;
