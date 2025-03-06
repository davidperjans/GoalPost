import React from 'react';
import { Link } from 'react-router-dom';
import { Button } from './ui/button';

const LandingPage: React.FC = () => {
  return (
    <div className="min-h-screen bg-gradient-to-br from-gray-50 via-white to-gray-100 dark:from-gray-900 dark:via-gray-800 dark:to-gray-900">
      <div className="container mx-auto px-4 py-16">
        <div className="flex flex-col items-center justify-center text-center">
          <div className="inline-flex items-center justify-center w-20 h-20 rounded-full bg-gradient-to-br from-green-400 to-green-500 hover:from-green-500 hover:to-green-600 transition-all duration-200 mb-8">
            <svg
              className="w-10 h-10 text-white"
              xmlns="http://www.w3.org/2000/svg"
              viewBox="0 0 24 24"
              fill="none"
              stroke="currentColor"
              strokeWidth="2"
              strokeLinecap="round"
              strokeLinejoin="round"
            >
              <path d="M12 2v20M2 12h20" />
              <circle cx="12" cy="12" r="10" />
            </svg>
          </div>

          <h1 className="text-4xl md:text-5xl font-bold text-gray-900 dark:text-white mb-6">
            Välkommen till AwayDayz
          </h1>

          <p className="text-xl text-gray-600 dark:text-gray-300 mb-8 max-w-2xl">
            Din personliga målhanteringsplattform för att spåra, uppnå och fira dina mål. 
            Börja din resa mot framgång idag!
          </p>

          <div className="flex flex-col sm:flex-row gap-4">
            <Link to="/register">
              <Button className="w-full sm:w-auto px-8 py-3 text-lg font-medium text-white bg-green-400 hover:bg-green-500 rounded-lg transition-colors duration-300">
                Kom igång
              </Button>
            </Link>
            <Link to="/login">
              <Button className="w-full sm:w-auto px-8 py-3 text-lg font-medium text-green-500 bg-white hover:bg-gray-50 border-2 border-green-400 rounded-lg transition-colors duration-300">
                Logga in
              </Button>
            </Link>
          </div>

          <div className="mt-16 grid grid-cols-1 md:grid-cols-3 gap-8 max-w-4xl">
            <div className="p-6 bg-white/90 dark:bg-gray-800/90 backdrop-blur-sm rounded-xl shadow-lg">
              <div className="w-12 h-12 bg-green-400 rounded-lg flex items-center justify-center mb-4">
                <svg className="w-6 h-6 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M9 5H7a2 2 0 00-2 2v12a2 2 0 002 2h10a2 2 0 002-2V7a2 2 0 00-2-2h-2M9 5a2 2 0 002 2h2a2 2 0 002-2M9 5a2 2 0 012-2h2a2 2 0 012 2" />
                </svg>
              </div>
              <h3 className="text-xl font-semibold text-gray-900 dark:text-white mb-2">Spåra dina mål</h3>
              <p className="text-gray-600 dark:text-gray-300">
                Skapa och hantera dina mål på ett enkelt och visuellt sätt
              </p>
            </div>

            <div className="p-6 bg-white/90 dark:bg-gray-800/90 backdrop-blur-sm rounded-xl shadow-lg">
              <div className="w-12 h-12 bg-green-400 rounded-lg flex items-center justify-center mb-4">
                <svg className="w-6 h-6 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M13 10V3L4 14h7v7l9-11h-7z" />
                </svg>
              </div>
              <h3 className="text-xl font-semibold text-gray-900 dark:text-white mb-2">Uppnå framgång</h3>
              <p className="text-gray-600 dark:text-gray-300">
                Följ din utveckling och fira dina framsteg längs vägen
              </p>
            </div>

            <div className="p-6 bg-white/90 dark:bg-gray-800/90 backdrop-blur-sm rounded-xl shadow-lg">
              <div className="w-12 h-12 bg-green-400 rounded-lg flex items-center justify-center mb-4">
                <svg className="w-6 h-6 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M17 20h5v-2a3 3 0 00-5.356-1.857M17 20H7m10 0v-2c0-.656-.126-1.283-.356-1.857M7 20H2v-2a3 3 0 015.356-1.857M7 20v-2c0-.656.126-1.283.356-1.857m0 0a5.002 5.002 0 019.288 0M15 7a3 3 0 11-6 0 3 3 0 016 0zm6 3a2 2 0 11-4 0 2 2 0 014 0zM7 10a2 2 0 11-4 0 2 2 0 014 0z" />
                </svg>
              </div>
              <h3 className="text-xl font-semibold text-gray-900 dark:text-white mb-2">Dela med dig</h3>
              <p className="text-gray-600 dark:text-gray-300">
                Dela dina framgångar med vänner och familj
              </p>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default LandingPage; 