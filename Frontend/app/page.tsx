const HomePage = async () => {
  return (
    <main className="flex flex-col space-y-8 font-sans">
      <div className="w-full lg:w-1/2 mx-auto">
        <h1 className="text-6xl py-6 text-center tracking-tighter font-mono text-transparent bg-clip-text bg-gradient-to-r from-sky-400 via-blue-500 to-indigo-600">
          LoanMe
        </h1>
      </div>

      <div className="rounded-md border border-gray-800 px-5 py-8 bg-gradient-to-t from-gray-950 text-gray-300 leading-relaxed">
        <div className="flex flex-col space-y-3 mb-5">
          <p>This application uses technologies and design patterns such as:</p>
        </div>

        <ul className="list-disc list-inside ml-1">
          <li>React</li>
          <li>Next.js</li>
          <li>Tailwind CSS</li>
          <li>.NET 10</li>
          <li>Entity Framework Core</li>
          <li>Own Mediator service approach</li>
          <li>Command-Query Responsibility Seggregation (CQRS)</li>
          <li>Clean Architecture + Vertical Sliced Architecture</li>
        </ul>
      </div>
    </main>
  );
};

export default HomePage;
