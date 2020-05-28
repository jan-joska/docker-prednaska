
FROM mcr.microsoft.com/mssql/server:2017-latest
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=ComplexPassword123
LABEL version=2017
EXPOSE 1433/tcp
RUN mkdir /database
RUN echo "tesdt" > /database/readme.txt"
VOLUME /database