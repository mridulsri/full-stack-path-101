import { BrowserRouter } from 'react-router-dom'
import { Navbar } from './components'
import { motion } from 'framer-motion'

const App = () => {
  return (
    <BrowserRouter>
      <div className='relative z-0 bg-primary'>
        <div className='bg-hero-pattern bg-cover bg-no-repeat bg-center'>
          <Navbar />
          <div className='relative z-0'>
            <motion.div
              initial={{ opacity: 0 }}
              animate={{ opacity: 1 }}
              transition={{ duration: 0.5 }}
              className='container mx-auto px-4 py-16'
            >
              <h1 className={`text-white font-black md:text-[60px] sm:text-[50px] xs:text-[40px] text-[30px]`}>
                Coming Soon
              </h1>
            </motion.div>
          </div>
        </div>
      </div>
    </BrowserRouter>
  )
}

export default App
