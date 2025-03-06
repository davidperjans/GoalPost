import React, { useState, useEffect } from 'react';
import { Card, CardContent } from './ui/card';
import { Button } from './ui/button';
import store from '../store/useAuthStore';
import { useNavigate } from 'react-router-dom';

const MainPage: React.FC = () => {
  const navigate = useNavigate();
  const [isProfileMenuOpen, setIsProfileMenuOpen] = useState(false);
  const [user, setUser] = useState(store.getState().user);

  useEffect(() => {
    const unsubscribe = store.subscribe((state) => {
      setUser(state.user);
    });

    return () => unsubscribe();
  }, []);

  if (!user) {
    navigate('/login');
    return null;
  }

  const handleLogout = () => {
    store.getState().logout();
    navigate('/login');
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-gray-50 via-white to-gray-100 dark:from-gray-900 dark:via-gray-800 dark:to-gray-900">
      {/* Header Section */}
      <div className="container mx-auto px-4 py-6">
        <div className="flex justify-between items-center">
          <div className="flex items-center space-x-4">
            <div className="w-12 h-12 rounded-full bg-gradient-to-br from-green-400 to-green-500 flex items-center justify-center">
              <svg
                className="w-6 h-6 text-white"
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
            <h1 className="text-2xl font-bold text-gray-900 dark:text-white">AwayDayz</h1>
          </div>
          
          <div className="flex items-center space-x-4">
            <Button 
              className="bg-green-400 hover:bg-green-500 text-white rounded-full px-6 py-2 flex items-center space-x-2 shadow-sm hover:shadow-md transition-all duration-200"
            >
              <svg
                className="w-5 h-5"
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                fill="none"
                stroke="currentColor"
                strokeWidth="2"
                strokeLinecap="round"
                strokeLinejoin="round"
              >
                <line x1="12" y1="5" x2="12" y2="19"></line>
                <line x1="5" y1="12" x2="19" y2="12"></line>
              </svg>
              <span>Lägg till besök</span>
            </Button>
            
            <div className="relative">
              <button
                onClick={() => setIsProfileMenuOpen(!isProfileMenuOpen)}
                className="flex items-center space-x-2 focus:outline-none"
              >
                <img
                  src={user.profileImage || 'https://via.placeholder.com/40'}
                  alt={user.username}
                  className="w-10 h-10 rounded-full"
                />
                <span className="text-gray-700 dark:text-gray-300">{user.username}</span>
                <svg
                  className={`w-4 h-4 text-gray-500 transform transition-transform ${isProfileMenuOpen ? 'rotate-180' : ''}`}
                  fill="none"
                  stroke="currentColor"
                  viewBox="0 0 24 24"
                >
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M19 9l-7 7-7-7" />
                </svg>
              </button>

              {isProfileMenuOpen && (
                <div className="absolute right-0 mt-2 w-48 bg-white dark:bg-gray-800 rounded-lg shadow-lg py-2 z-50">
                  <button
                    onClick={() => {
                      setIsProfileMenuOpen(false);
                      // Navigera till profil
                    }}
                    className="block w-full text-left px-4 py-2 text-sm text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-700"
                  >
                    Min profil
                  </button>
                  <button
                    onClick={() => {
                      setIsProfileMenuOpen(false);
                      // Navigera till inställningar
                    }}
                    className="block w-full text-left px-4 py-2 text-sm text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-700"
                  >
                    Inställningar
                  </button>
                  <button
                    onClick={handleLogout}
                    className="block w-full text-left px-4 py-2 text-sm text-red-600 hover:bg-gray-100 dark:hover:bg-gray-700"
                  >
                    Logga ut
                  </button>
                </div>
              )}
            </div>
          </div>
        </div>
      </div>

      {/* Stats Section */}
      <div className="container mx-auto px-4 py-8">
        <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
          <Card className="border-0 shadow-lg bg-white/90 dark:bg-gray-800/90 backdrop-blur-sm">
            <CardContent className="p-6">
              <div className="text-3xl font-bold text-green-500 mb-2">12</div>
              <div className="text-gray-600 dark:text-gray-300">Besökta arenor</div>
            </CardContent>
          </Card>

          <Card className="border-0 shadow-lg bg-white/90 dark:bg-gray-800/90 backdrop-blur-sm">
            <CardContent className="p-6">
              <div className="text-3xl font-bold text-green-500 mb-2">5</div>
              <div className="text-gray-600 dark:text-gray-300">Länder</div>
            </CardContent>
          </Card>

          <Card className="border-0 shadow-lg bg-white/90 dark:bg-gray-800/90 backdrop-blur-sm">
            <CardContent className="p-6">
              <div className="text-3xl font-bold text-green-500 mb-2">3</div>
              <div className="text-gray-600 dark:text-gray-300">Achievements</div>
            </CardContent>
          </Card>
        </div>
      </div>

      {/* Recent Visits */}
      <div className="container mx-auto px-4 py-8">
        <h2 className="text-2xl font-bold text-gray-900 dark:text-white mb-6">Senaste besök</h2>
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          <Card className="border-0 shadow-lg bg-white/90 dark:bg-gray-800/90 backdrop-blur-sm hover:shadow-xl transition-all duration-300">
            <CardContent className="p-6">
              <div className="aspect-video bg-gray-200 dark:bg-gray-700 rounded-lg mb-4"></div>
              <h3 className="text-xl font-semibold text-gray-900 dark:text-white mb-2">Friends Arena</h3>
              <p className="text-gray-600 dark:text-gray-300 mb-4">Stockholm, Sverige</p>
              <div className="flex items-center justify-between">
                <div className="flex items-center space-x-2">
                  <div className="w-8 h-8 rounded-full bg-green-400"></div>
                  <span className="text-sm text-gray-600 dark:text-gray-300">4.5/5</span>
                </div>
                <span className="text-sm text-gray-500 dark:text-gray-400">12 Mar 2024</span>
              </div>
            </CardContent>
          </Card>

          <Card className="border-0 shadow-lg bg-white/90 dark:bg-gray-800/90 backdrop-blur-sm hover:shadow-xl transition-all duration-300">
            <CardContent className="p-6">
              <div className="aspect-video bg-gray-200 dark:bg-gray-700 rounded-lg mb-4"></div>
              <h3 className="text-xl font-semibold text-gray-900 dark:text-white mb-2">Wembley Stadium</h3>
              <p className="text-gray-600 dark:text-gray-300 mb-4">London, England</p>
              <div className="flex items-center justify-between">
                <div className="flex items-center space-x-2">
                  <div className="w-8 h-8 rounded-full bg-green-400"></div>
                  <span className="text-sm text-gray-600 dark:text-gray-300">5/5</span>
                </div>
                <span className="text-sm text-gray-500 dark:text-gray-400">5 Mar 2024</span>
              </div>
            </CardContent>
          </Card>

          <Card className="border-0 shadow-lg bg-white/90 dark:bg-gray-800/90 backdrop-blur-sm hover:shadow-xl transition-all duration-300">
            <CardContent className="p-6">
              <div className="aspect-video bg-gray-200 dark:bg-gray-700 rounded-lg mb-4"></div>
              <h3 className="text-xl font-semibold text-gray-900 dark:text-white mb-2">Allianz Arena</h3>
              <p className="text-gray-600 dark:text-gray-300 mb-4">München, Tyskland</p>
              <div className="flex items-center justify-between">
                <div className="flex items-center space-x-2">
                  <div className="w-8 h-8 rounded-full bg-green-400"></div>
                  <span className="text-sm text-gray-600 dark:text-gray-300">4/5</span>
                </div>
                <span className="text-sm text-gray-500 dark:text-gray-400">1 Mar 2024</span>
              </div>
            </CardContent>
          </Card>
        </div>
      </div>

      {/* Map Preview */}
      <div className="container mx-auto px-4 py-8">
        <Card className="border-0 shadow-lg bg-white/90 dark:bg-gray-800/90 backdrop-blur-sm">
          <CardContent className="p-6">
            <h2 className="text-2xl font-bold text-gray-900 dark:text-white mb-6">Din resa</h2>
            <div className="aspect-video bg-gray-200 dark:bg-gray-700 rounded-lg">
              {/* Här kommer kartan att implementeras */}
            </div>
          </CardContent>
        </Card>
      </div>
    </div>
  );
};

export default MainPage; 