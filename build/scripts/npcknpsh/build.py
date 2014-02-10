import buildconfig

class Build:
    
    def __init__(self, config = None):
        self.config = config or buildconfig

    def project_class(self):
        return self.config.project_class

    def project_dir(self):
        return self.config.project_dir

    def process(self):

        project_dir = self.project_dir()
        project_class = self.project_class()

        project = project_class(project_dir)
        project.pack()

if __name__ == '__main__':
    Build().process()