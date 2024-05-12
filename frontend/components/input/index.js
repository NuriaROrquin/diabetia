
export const Input = ({type, placeholder, id, width, icon}) => {
  return (
      <div className={`mb-4 ${width} flex items-center relative`}>
              {icon && (
                  <div className="absolute mr-2 w-10 text-gray-secondary pl-2">
                      {icon}
                  </div>
              )}
              <input
                  type={type}
                  id={id}
                  placeholder={placeholder}
                  className="border border-gray-400 rounded-lg p-3 pl-12 w-full"
              />
      </div>
  )
}