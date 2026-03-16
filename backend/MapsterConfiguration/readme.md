# MapsterConfiguration


all the configuration for mapping go here


https://github.com/MapsterMapper/Mapster




## Example 

    
        config.NewConfig<TestDto , UpdateTestCommand>()
                   .Map(destination => d.Phone, source => s.Phone )
                   .Map(destination => d.Name , source => s.Name);
                   

